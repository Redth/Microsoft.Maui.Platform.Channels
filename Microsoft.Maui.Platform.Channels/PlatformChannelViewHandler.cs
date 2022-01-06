using Microsoft.Maui.Platform.Channels;

namespace SamplePlatformChannels
{
    public partial class PlatformChannelViewHandler
    {
		public static PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewMapper = new()
		{
			//[nameof(IPlatformChannelView.ChannelId)] = MapChannelId,
		};
		
		public static CommandMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewCommandMapper = new()
		{
			//[nameof(IPlatformChannelView.SendToPlatform)] = MapSendToPlatform,
			//[nameof(IPlatformChannelView.ReceiveFromPlatform)] = MapReceiveFromPlatform,
		};

		public PlatformChannelViewHandler() : base(PlatformChannelViewMapper, PlatformChannelViewCommandMapper)
		{
		}

		
    }
}

