using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels.Controls;

public class PlatformChannelView : View, IPlatformChannelView
{
	public static BindableProperty ChannelIdProperty =
		BindableProperty.Create(
			nameof(ChannelTypeId),
			typeof(string),
			typeof(PlatformChannelView),
			defaultValue: default);

	public string ChannelTypeId
	{
		get => (string)GetValue(ChannelIdProperty);
		set => SetValue(ChannelIdProperty, value);
	}

	public object SendToPlatform(string messageId, params object[] parameters)
		=> (Handler?.VirtualView as IPlatformChannelView)?.SendToPlatform(messageId, parameters);

	public event ChannelMessageDelegate OnReceiveFromPlatform;

	public object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);
}
