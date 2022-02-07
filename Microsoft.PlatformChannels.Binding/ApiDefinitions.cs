//using DotNetPlatformChannels;
using Foundation;
using ObjCRuntime;
//using ObjectiveC;

namespace Microsoft.PlatformChannels.Platform
{
	interface IChannelProtocol { }
	// @protocol ChannelProtocol <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/
	[Protocol (Name = "_TtP22DotNetPlatformChannels15ChannelProtocol_")]
	[BaseType (typeof(NSObject), Name = "_TtP22DotNetPlatformChannels15ChannelProtocol_")]
	interface ChannelProtocol
	{
		// @required -(NSObject * _Nullable)handleMessageFromMauiWithMessageId:(NSString * _Nonnull)messageId parameters:(NSArray<NSObject *> * _Nullable)parameters __attribute__((warn_unused_result("")));
		[Abstract]
		[Export ("handleMessageFromMauiWithMessageId:parameters:")]
		[return: NullAllowed]
		NSObject HandleMessageFromMaui (string messageId, [NullAllowed] NSObject[] parameters);
	}

	
	// @interface Channel : NSObject <ChannelProtocol>
	[BaseType (typeof(NSObject), Name = "_TtC22DotNetPlatformChannels7Channel")]
	interface Channel : IChannelProtocol
	{
		// @property (nonatomic, strong) id<ChannelMessageHandler> _Nullable handler;
		[NullAllowed, Export ("handler", ArgumentSemantic.Strong)]
		IChannelMessageHandler Handler { get; set; }

		// -(NSObject * _Nullable)handleMessageFromMauiWithMessageId:(NSString * _Nonnull)messageId parameters:(NSArray<NSObject *> * _Nullable)parameters __attribute__((warn_unused_result("")));
		[Export ("handleMessageFromMauiWithMessageId:parameters:")]
		[return: NullAllowed]
		NSObject HandleMessageFromMaui (string messageId, [NullAllowed] NSObject[] parameters);
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
*/
	interface IChannelMessageHandler { }

	[Protocol (Name = "_TtP22DotNetPlatformChannels21ChannelMessageHandler_")]
	[BaseType (typeof(NSObject), Name = "_TtP22DotNetPlatformChannels21ChannelMessageHandler_")]
	interface ChannelMessageHandler
	{
		// @required -(NSObject * _Nullable)onChannelMessageWithMessageId:(NSString * _Nonnull)messageId parameters:(NSArray<NSObject *> * _Nonnull)parameters __attribute__((warn_unused_result("")));
		[Abstract]
		[Export ("onChannelMessageWithMessageId:parameters:")]
		[return: NullAllowed]
		NSObject OnChannelMessage (string messageId, NSObject[] parameters);
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
*/
	interface IChannelProvider { }

	[Protocol (Name = "_TtP22DotNetPlatformChannels15ChannelProvider_")]
	[BaseType (typeof(NSObject), Name = "_TtP22DotNetPlatformChannels15ChannelProvider_")]
	interface ChannelProvider
	{
		// @required -(NSString * _Nonnull)getDefaultInstanceId __attribute__((warn_unused_result("")));
		[Abstract]
		[Export ("getDefaultInstanceId")]
		string DefaultInstanceId { get; }

		// @required -(Channel * _Nonnull)getInstanceWithChannelId:(NSString * _Nonnull)channelId __attribute__((warn_unused_result("")));
		[Abstract]
		[Export ("getInstanceWithChannelId:")]
		Channel GetInstance (string channelId);

		// @required -(Channel * _Nonnull)getInstanceWithChannelId:(NSString * _Nonnull)channelId instanceId:(NSString * _Nonnull)instanceId __attribute__((warn_unused_result("")));
		[Abstract]
		[Export ("getInstanceWithChannelId:instanceId:")]
		Channel GetInstance (string channelId, string instanceId);
	}

	// @interface ChannelService : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC22DotNetPlatformChannels14ChannelService")]
	interface ChannelService
	{
		// @property (nonatomic, strong) id<ChannelProvider> _Nullable channelProvider;
		[Static]
		[NullAllowed, Export ("channelProvider", ArgumentSemantic.Strong)]
		IChannelProvider Provider { get; set; }

		// +(void)registerWithChannelId:(NSString * _Nonnull)channelId channelClass:(Class _Nonnull)channelClass;
		[Static]
		[Export ("registerWithChannelId:channelClass:")]
		void Register (string channelId, Class channelClass);

		// +(Channel * _Nullable)getOrCreateChannelWithChannelId:(NSString * _Nonnull)channelId error:(NSError * _Nullable * _Nullable)error __attribute__((warn_unused_result("")));
		[Static]
		[Export ("getOrCreateChannelWithChannelId:error:")]
		[return: NullAllowed]
		Channel GetOrCreateChannel (string channelId, [NullAllowed] out NSError error);

		// +(Channel * _Nullable)getOrCreateChannelWithChannelId:(NSString * _Nonnull)channelId instanceId:(NSString * _Nonnull)instanceId error:(NSError * _Nullable * _Nullable)error __attribute__((warn_unused_result("")));
		[Static]
		[Export ("getOrCreateChannelWithChannelId:instanceId:error:")]
		[return: NullAllowed]
		Channel GetOrCreateChannel (string channelId, string instanceId, [NullAllowed] out NSError error);

		// +(Channel * _Nullable)createWithChannelId:(NSString * _Nonnull)channelId error:(NSError * _Nullable * _Nullable)error __attribute__((warn_unused_result("")));
		[Static]
		[Export ("createWithChannelId:error:")]
		[return: NullAllowed]
		Channel Create (string channelId, [NullAllowed] out NSError error);
	}
}
