
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
		=> PlatformViewChannel.HandleMessageFromDotNet(messageId, parameters.ToPlatformObjects()).ToDotNetObject();
}