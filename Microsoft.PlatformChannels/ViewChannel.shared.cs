
using PlatformChannel = Microsoft.PlatformChannels.Platform.Channel;
using IPlatformChannelProvider = Microsoft.PlatformChannels.Platform.IChannelProvider;
using PlatformChannelService = Microsoft.PlatformChannels.Platform.ChannelService;
using PlatformViewChannel = Microsoft.PlatformChannels.Platform.ViewChannel;

using IPlatformChannelMessageHandler = Microsoft.PlatformChannels.Platform.IChannelMessageHandler;

#if IOS || MACCATALYST
using PlatformObject = Foundation.NSObject;
#elif ANDROID
using PlatformObject = Java.Lang.Object;
#endif

namespace Microsoft.PlatformChannels;

public partial class ViewChannel : PlatformObject, IPlatformChannelMessageHandler
{
	internal ViewChannel(PlatformViewChannel platformChannel)
	{
		PlatformViewChannel = platformChannel;
		PlatformViewChannel.SetManagedHandler(this);
	}

	public event ChannelMessageDelegate OnReceiveFromPlatform;

	internal PlatformViewChannel PlatformViewChannel { get; set; }

	public PlatformObject OnChannelMessage(string messageId, params PlatformObject[] parameters)
		=> ReceiveFromPlatform(messageId, parameters.ToDotNetObjects()).ToPlatformObject();

	public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);

	public object SendToPlatform(string messageId, params object[] parameters)
#if IOS || MACCATALYST
		=> (PlatformViewChannel as IPlatformChannelMessageHandler)?.OnChannelMessage(messageId, parameters.ToPlatformObjects()).ToDotNetObject();
#else
		=> PlatformViewChannel.HandleMessageFromDotNet(messageId, parameters.ToPlatformObjects()).ToDotNetObject();
#endif



#if IOS || MACCATALYST
	public virtual UIKit.UIView GetPlatformView()
		=> PlatformViewChannel?.GetPlatformView();
#elif ANDROID
	public Android.Content.Context Context { get; set; }

	public virtual Android.Views.View GetPlatformView()
		=> PlatformViewChannel?.GetPlatformView(Context);
#endif
}