
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
		=> ToPlatformObject(ReceiveFromPlatform(messageId, ToDotNetObjects(parameters)));

	public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);

	public object SendToPlatform(string messageId, params object[] parameters)
		=> ToDotNetObject(PlatformChannel.HandleMessageFromDotNet(messageId, ToPlatformObjects(parameters)));
}

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
		=> Channel.ToPlatformObject(ReceiveFromPlatform(messageId, Channel.ToDotNetObjects(parameters)));

	public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);

	public object SendToPlatform(string messageId, params object[] parameters)
		=> Channel.ToDotNetObject(PlatformViewChannel.HandleMessageFromDotNet(messageId, Channel.ToPlatformObjects(parameters)));
}