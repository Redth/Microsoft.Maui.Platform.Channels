namespace Microsoft.Maui.Platform.Channels;

public static class PlatformChannelHostingExtensions
{

    public static MauiAppBuilder UsePlatformChannels(this MauiAppBuilder builder, ChannelBrokerConfiguration config = null)
    {
        builder.Services.AddSingleton(_ => new ChannelBroker(config));
        
        return builder;
    }

}
