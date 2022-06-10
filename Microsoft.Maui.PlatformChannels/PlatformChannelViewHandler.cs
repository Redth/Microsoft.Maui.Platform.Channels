using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels
{
	public partial class PlatformChannelViewHandler : Microsoft.Maui.Handlers.ViewHandler<IPlatformChannelView, PlatformView>
	{
		public static PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewMapper = new PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler>(PlatformChannelViewHandler.ViewMapper)
		{
		};

		public static CommandMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewCommandMapper = new(PlatformChannelViewHandler.ViewCommandMapper)
		{
		};

		public PlatformChannelViewHandler() : base(PlatformChannelViewMapper, PlatformChannelViewCommandMapper)
		{

		}

		public event ChannelMessageDelegate OnReceivedFromPlatform;
	}
}

