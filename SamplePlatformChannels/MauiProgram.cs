using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform.Channels;

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
            })
            .UsePlatformChannels(
#if ANDROID
                new ChannelBrokerConfiguration("com.myapp", "MyClass", "Init")
#endif
                );

        return builder.Build();
    }
}


