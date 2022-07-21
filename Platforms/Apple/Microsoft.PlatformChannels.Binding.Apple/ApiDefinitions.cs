using Foundation;
using ObjCRuntime;
using UIKit;

namespace Microsoft.PlatformChannels.Platform
{
    interface IChannelMessageHandler { }

    [Protocol]
    [BaseType(typeof(NSObject), Name = "_TtP22DotNetPlatformChannels21ChannelMessageHandler_")]
    interface ChannelMessageHandler
    {
        [Abstract]
        [Export("onChannelMessage:withArgs:")]
        [return: NullAllowed]
        NSObject OnChannelMessage(string messageId, [NullAllowed] NSObject[] args);
    }

    [BaseType(typeof(NSObject), Name = "_TtC22DotNetPlatformChannels7Channel")]
    interface Channel : ChannelMessageHandler
    {
        [Export("setManagedHandler:", ArgumentSemantic.Weak)]
        void SetManagedHandler(IChannelMessageHandler handler);

        [Export("sendMessageToDotNet:withArgs:")]
        NSObject SendMessageToDotNet(string messageId, NSObject[] args);
    }

    interface IChannelProvider { }

    [Protocol]
    [BaseType(typeof(NSObject), Name = "_TtP22DotNetPlatformChannels15ChannelProvider_")]
    interface ChannelProvider
    {
        [Abstract]
        [Export("getDefaultInstanceId")]
        string DefaultInstanceId { get; }

        [Abstract]
        [Export("getInstance:withInstance:")]
        Channel GetInstance(string channelId, string instanceId);

        [Abstract]
        [Export("disposeInstance:withInstance:")]
        void DisposeInstance(string channelId, string instanceId);
    }

    [BaseType(typeof(NSObject), Name = "_TtC22DotNetPlatformChannels14ChannelService")]
    interface ChannelService
    {
        [Export("getOrCreate:instance:")]
        [Static]
        Channel GetOrCreate(string channelId, string instanceId);

        [Export("create:")]
        [Static]
        Channel Create(string channelId);

        [NullAllowed, Export("channelProvider", ArgumentSemantic.Weak)]
        [Static]
        IChannelProvider Provider { get; set; }
    }

    [BaseType(typeof(Channel), Name = "_TtC22DotNetPlatformChannels11ViewChannel")]
    interface ViewChannel : Channel
    {
        [Export("getPlatformView")]
        UIView GetPlatformView();
    }
}