//using SamplePlatformChannels;
//using Object = Java.Lang.Object;

//namespace Microsoft.Maui.Platform.Channels.Controls;

//public class PlatformChannelView : View, IPlatformChannelView
//{
//    public static BindableProperty BrokerProperty =
//        BindableProperty.Create(
//            nameof(Broker), 
//            typeof(Channels),
//            typeof(PlatformChannelView),
//            defaultValue: default);
    
//    public Channels Broker
//    {
//        get => (Channels)GetValue(BrokerProperty);
//        set => SetValue(BrokerProperty, value);
//    }
    
//    public static BindableProperty ChannelIdProperty =
//        BindableProperty.Create(
//            nameof(ChannelTypeId), 
//            typeof(string),
//            typeof(PlatformChannelView),
//            defaultValue: default);

//    public string ChannelTypeId
//    {
//        get => (string)GetValue(ChannelIdProperty);
//        set => SetValue(ChannelIdProperty, value);
//    }
    
//    public object SendToPlatform(string messageId, params object[] parameters)
//        => (Handler?.VirtualView as IPlatformChannelView)?.SendToPlatform(messageId, parameters);

//    public event ChannelMessageDelegate OnReceiveFromPlatform;
    
//    public object ReceiveFromPlatform(string messageId, params object[] parameters)
//        => OnReceiveFromPlatform?.Invoke(messageId, parameters);
//}
