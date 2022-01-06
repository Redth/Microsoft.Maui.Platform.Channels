namespace Microsoft.Maui.Platform.Channels;

public partial class ChannelBrokerConfiguration
{
    public ChannelBrokerConfiguration()
    {
    }
}

public delegate object ChannelMessageDelegate(string messageId, params object[] parameters);
