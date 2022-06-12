using Microsoft.Maui.Handlers;
using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels
{
	public partial class PlatformChannelViewHandler : Microsoft.Maui.Handlers.ViewHandler<IPlatformChannelView, PlatformView>
	{
		public static PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewViewMapper = new PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler>(ViewHandler.ViewMapper)
		{
			[nameof(PlatformChannelView.ChannelTypeId)] = MapChannelTypeId
		};

		public static CommandMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewCommandMapper = new(PlatformChannelViewHandler.ViewCommandMapper)
		{
		};

		public PlatformChannelViewHandler() : base(PlatformChannelViewViewMapper, PlatformChannelViewCommandMapper)
		{
			Init();
		}

		public PlatformChannelViewHandler(PropertyMapper propertyMapper, CommandMapper commandMapper) : base(propertyMapper ?? PlatformChannelViewViewMapper, commandMapper ?? PlatformChannelViewCommandMapper)
		{
			Init();
		}

		protected IChannelService ChannelService { get; private set; }

		void Init()
		{
			
		}

		public event ChannelMessageDelegate OnReceivedFromPlatform;


	}
}

