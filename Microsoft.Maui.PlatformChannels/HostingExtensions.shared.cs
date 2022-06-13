using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels;

public static class PlatformChannelHostingExtensions
{
	public static MauiAppBuilder UsePlatformChannels(this MauiAppBuilder builder, ChannelServiceConfiguration config = null)
	{
		builder.Services.AddSingleton<IChannelService>(_ => new ChannelService(config));

		return builder;
	}

}