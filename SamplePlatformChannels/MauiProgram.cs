using Microsoft.PlatformChannels;

namespace SamplePlatformChannels;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });


        ChannelServiceConfiguration? config = null;
#if ANDROID
        config = new ChannelServiceConfiguration("com.microsoft.maui.platform.channels.sample", "SampleChannels", "init");
#elif IOS || MACCATALYST
        config = new ChannelServiceConfiguration("", "");
#endif

        if (config != null)
            builder.Services.AddSingleton<IChannelService>(_ => new ChannelService(config));

        return builder.Build();
    }
}


