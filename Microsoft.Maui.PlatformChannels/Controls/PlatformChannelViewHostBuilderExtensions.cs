using Microsoft.Extensions.DependencyInjection;
using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels.Controls;

public static class PlatformChannelViewHostBuilderExtensions
{
	public static MauiAppBuilder UseMauiPlatformChannelViews(this MauiAppBuilder builder, ChannelServiceConfiguration config)
	{
		if (config != null)
			builder.Services.AddSingleton<IChannelService>(_ => new ChannelService(config));

		builder.ConfigureMauiHandlers(handlers =>
			handlers.AddHandler(typeof(IPlatformChannelView), typeof(PlatformChannelViewHandler)));

		return builder;
	}
}
