using Microsoft.Maui.PlatformChannels.Controls;
using Microsoft.PlatformChannels;

using Microsoft.Extensions.Logging;

namespace SamplePlatformChannels;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		ChannelServiceConfiguration? config = null;
#if ANDROID
		config = new ChannelServiceConfiguration("com.microsoft.dotnet.platformchannels.sample", "SampleChannels", "init");
#elif IOS || MACCATALYST
		config = new ChannelServiceConfiguration("Sample.SampleChannels", "initChannels");
#endif

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiPlatformChannelViews(config)
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
