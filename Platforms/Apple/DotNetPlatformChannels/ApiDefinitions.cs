using DotNetPlatformChannels;
using Foundation;
using ObjCRuntime;
using UIKit;

[Static]
[Verify (ConstantsInterfaceAssociation)]
partial interface Constants
{
	// extern double DotNetPlatformChannelsVersionNumber;
	[Field ("DotNetPlatformChannelsVersionNumber", "__Internal")]
	double DotNetPlatformChannelsVersionNumber { get; }

	// extern const unsigned char [] DotNetPlatformChannelsVersionString;
	[Field ("DotNetPlatformChannelsVersionString", "__Internal")]
	byte[] DotNetPlatformChannelsVersionString { get; }
}

// @protocol ChannelMessageHandler <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface ChannelMessageHandler
{
	// @required -(id _Nullable)onChannelMessage:(NSString * _Nonnull)messageId withArgs:(NSArray * _Nullable)args;
	[Abstract]
	[Export ("onChannelMessage:withArgs:")]
	[Verify (StronglyTypedNSArray)]
	[return: NullAllowed]
	NSObject WithArgs (string messageId, [NullAllowed] NSObject[] args);
}

// @interface Channel : NSObject
[BaseType (typeof(NSObject))]
interface Channel
{
	// @property (weak) id<ChannelMessageHandler> _Nullable managedHandler;
	[NullAllowed, Export ("managedHandler", ArgumentSemantic.Weak)]
	ChannelMessageHandler ManagedHandler { get; set; }

	// -(id _Nonnull)sendMessageToDotNet:(NSString * _Nonnull)messageId withArgs:(NSArray * _Nonnull)args;
	[Export ("sendMessageToDotNet:withArgs:")]
	[Verify (StronglyTypedNSArray)]
	NSObject SendMessageToDotNet (string messageId, NSObject[] args);
}

// @protocol ChannelProvider <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface ChannelProvider
{
	// @required -(NSString * _Nonnull)getDefaultInstanceId;
	[Abstract]
	[Export ("getDefaultInstanceId")]
	[Verify (MethodToProperty)]
	string DefaultInstanceId { get; }

	// @required -(Channel<ChannelMessageHandler> * _Nonnull)getInstance:(NSString * _Nonnull)channelId withInstance:(NSString * _Nonnull)instanceId;
	[Abstract]
	[Export ("getInstance:withInstance:")]
	ChannelMessageHandler GetInstance (string channelId, string instanceId);

	// @required -(void)disposeInstance:(NSString * _Nonnull)channelId withInstance:(NSString * _Nonnull)instanceId;
	[Abstract]
	[Export ("disposeInstance:withInstance:")]
	void DisposeInstance (string channelId, string instanceId);
}

// @interface ChannelService : NSObject
[BaseType (typeof(NSObject))]
interface ChannelService
{
	// +(ChannelService * _Nonnull)getInstance;
	[Static]
	[Export ("getInstance")]
	[Verify (MethodToProperty)]
	ChannelService Instance { get; }

	// +(void)registerChannel:(NSString * _Nonnull)channelId channelType:(Class _Nonnull)type;
	[Static]
	[Export ("registerChannel:channelType:")]
	void RegisterChannel (string channelId, Class type);

	// +(Channel<ChannelMessageHandler> * _Nonnull)getOrCreate:(NSString * _Nonnull)channelId instance:(NSString * _Nonnull)instanceId;
	[Static]
	[Export ("getOrCreate:instance:")]
	ChannelMessageHandler GetOrCreate (string channelId, string instanceId);

	// +(Channel<ChannelMessageHandler> * _Nonnull)create:(NSString * _Nonnull)channelId;
	[Static]
	[Export ("create:")]
	ChannelMessageHandler Create (string channelId);

	// @property (nonatomic, weak) id<ChannelProvider> _Nullable channelProvider;
	[NullAllowed, Export ("channelProvider", ArgumentSemantic.Weak)]
	ChannelProvider ChannelProvider { get; set; }
}

// @protocol ChannelViewProvider <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
[BaseType (typeof(NSObject))]
interface ChannelViewProvider
{
	// @required -(UIView *)getPlatformView;
	[Abstract]
	[Export ("getPlatformView")]
	[Verify (MethodToProperty)]
	UIView PlatformView { get; }
}

// @interface ViewChannel : Channel
[BaseType (typeof(Channel))]
interface ViewChannel
{
}
