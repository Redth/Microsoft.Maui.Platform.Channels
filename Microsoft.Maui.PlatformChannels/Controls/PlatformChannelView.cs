using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels.Controls;

public class PlatformChannelView : View, IPlatformChannelView
{
	public static BindableProperty ChannelTypeIdProperty =
		BindableProperty.Create(
			nameof(ChannelTypeId),
			typeof(string),
			typeof(PlatformChannelView),
			defaultValue: default);

	public string ChannelTypeId
	{
		get => (string)GetValue(ChannelTypeIdProperty);
		set => SetValue(ChannelTypeIdProperty, value);
	}

	public static BindableProperty ChannelInstanceIdProperty =
		BindableProperty.Create(
			nameof(ChannelInstanceId),
			typeof(string),
			typeof(PlatformChannelView),
			defaultValue: default);

	public string ChannelInstanceId
	{
		get => (string)GetValue(ChannelInstanceIdProperty);
		set => SetValue(ChannelInstanceIdProperty, value);
	}

	public object SendToPlatform(string messageId, params object[] parameters)
		=> (Handler as PlatformChannelViewHandler)?.SendToPlatformImpl(messageId, parameters);

	public event ChannelMessageDelegate OnReceiveFromPlatform;

	public object ReceiveFromPlatform(string messageId, params object[] parameters)
		=> OnReceiveFromPlatform?.Invoke(messageId, parameters);
}
