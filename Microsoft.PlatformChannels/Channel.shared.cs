
using IPlatformChannelProvider = Microsoft.PlatformChannels.Platform.IChannelProvider;
using PlatformChannelService = Microsoft.PlatformChannels.Platform.ChannelService;

using IPlatformChannelMessageHandler = Microsoft.PlatformChannels.Platform.IChannelMessageHandler;

#if IOS || MACCATALYST
using PlatformChannel = Microsoft.PlatformChannels.Platform.Channel;
using PlatformObject = Foundation.NSObject;
#elif ANDROID
using PlatformChannel = Microsoft.PlatformChannels.Platform.Channel;
using PlatformObject = Java.Lang.Object;
#endif

namespace Microsoft.PlatformChannels;

public partial class Channel : PlatformObject, IPlatformChannelMessageHandler
{
	internal Channel(PlatformChannel platformChannel)
	{
		PlatformChannel = platformChannel;
		PlatformChannel.SetManagedHandler(this);
	}

	public event ChannelMessageDelegate OnReceiveFromPlatform;

	internal PlatformChannel PlatformChannel { get; set; }

	public PlatformObject OnChannelMessage(string messageId, params PlatformObject[] parameters)
		=> ReceiveFromPlatform(messageId, parameters.ToDotNetObjects()).ToPlatformObject();

	public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);

	public object SendToPlatform(string messageId, params object[] parameters)
#if IOS || MACCATALYST
		=> (PlatformChannel as IPlatformChannelMessageHandler)?.OnChannelMessage(messageId, parameters.ToPlatformObjects()).ToDotNetObject();
#else
		=> PlatformChannel.HandleMessageFromDotNet(messageId, parameters.ToPlatformObjects()).ToDotNetObject();
#endif
}
