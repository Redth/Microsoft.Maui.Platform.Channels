Microsoft.Maui.Platform.Channels is a complex project, with many moving parts.

This design overview seeks to describe the product and API structure to help ground your understanding of how they fit together.

# Project Structure

This project is built upon a native library, one for iOS and another for Android:

- Android (java): Platforms/Android
- iOS (Objective-C): Platforms/Apple

Those native libraries are bound in binding projects to be callable from c#:

- Android: Microsoft.PlatformChannels.Binding/Microsoft.PlatformChannels.Binding.csproj
  - There are some iOS references here, but it is not used on iOS today
- iOS: Microsoft.PlatformChannels.Binding.Apple/Microsoft.PlatformChannels.Binding.Apple.csproj

The PlatformChannel abstraction is built using C# (leveraging those native libraries) in two layers:

- Microsoft.PlatformChannels/Microsoft.PlatformChannels.csproj provides the basic features without any references to MAUI
- Microsoft.Maui.PlatformChannels/Microsoft.Maui.PlatformChannels.csproj builds on top of Microsoft.PlatformChannels and provides MAUI specific functionality on top

Finally, a MAUI sample project shows everything together:

- SamplePlatformChannels/SamplePlatformChannels.csproj

# API

Before discussing each type in the API, there is a pattern that runs throughout that will be useful to understand.

Many classes are mirrored, with either a managed and native instance (Channel, ChannelService), or a fully managed item mirrored by a native interface for calling into managed code (ChannelProvider).

This mirroring is separate to the binding project definition of the native class, which for iOS would look like a third 'copy'.

## Channel (Managed & Native)

The most fundamental concept in the API is a `Channel`. A Channel is a bi-directional messaging object, going from managed code to/from native code.

Each message contains a string message ID along with a variable array of arguments boxed in the native 'object' type of the platform (Object or NSObject).

An instance of the Channel is made up of both a C# Channel class and a native Channel class.

The C# side holds a strong reference to the native instance.

The native side holds onto weak reference back, stored within a `ChannelMessageHandler` type, which expresses:

`public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)`

in a platform callable way used to send messages back to managed code.

Users derive classes from Channel (or a subclass) to expose platform functionality to C# with an easily consumed standard interface.

## ChannelProvider (Managed with Native Interface)

A `ChannelProvider` is a managed C# object which stores all active channels and manages their lifetimes.

The core data structure is effectively:

```csharp
Dictionary<ChannelID, Dictionary<InstanceID, ChannelInstanceHolder>> channels;
```

Each channel can have many instances, and each instance maps to one specific pair of Native Channel / Managed Channel and associated metadata (ChannelInstanceHolder).

The `ChannelProvider` is exposed into native code as an interface with methods to fetch and disposed of a given ChannelID/InstanceID pair.

## ChannelService (Managed & Native)

A `ChannelService` is the public factory which users to register and then create `Channel` to interact with. In managed code it is an implied static (using the MAUI Dependency Injection pattern) and in native code it is an actual static.

It contains both a managed and native ends:

- Native code calls into ChannelService to register channelIDs against user written `Channel` subclasses.
- Managed code then creates channels from the ChannelService based on those channelIDs and sends messages over the channel.

## ChannelServiceConfiguration

As part of starting up a `ChannelService` a `ChannelServiceConfiguration` is passed in.

A ChannelServiceConfiguration contains native of a static native package/class/method to invoke. This method will then register with the native ChannelService class all channels provided by the binding.

## ViewChannel

A `ViewChannel` is a subclass of `Channel` which provides a simplified API for passing a platform view (android.view.View or UIView) from managed to native.

# Example Call Lifecycle

In this walk through all types are the managed version, unless otherwise stated.

During Main startup or in CreateMauiApp a ChannelServiceConfiguration is configured to an entry point specific to the native library containing a channel.

Possibly indirectly with `UseMauiPlatformChannelViews` a new ChannelService is instanced with that configuration.

During ChannelService's constructor the native method configured is invoked, which registers with the native ChannelService global.

At some point, either due to direct calls against the ChannelService or indirect calls via XML bindings, Channels will be created from the ChannelService. Each channel will register itself with the ChannelProvider which ensures everything stays in sync until an explicit dispose call.
