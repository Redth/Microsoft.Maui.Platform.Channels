using Microsoft.Maui.Handlers;
using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels;

public partial class PlatformChannelViewHandler : ViewHandler<IPlatformChannelView, PlatformView>
{
	public static PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewViewMapper = new PropertyMapper<IPlatformChannelView, PlatformChannelViewHandler>(ViewMapper)
	{
		[nameof(IPlatformChannelView.ChannelTypeId)] = MapChannelTypeId,
		[nameof(IPlatformChannelView.ChannelInstanceId)] = MapChannelInstanceId
	};

	public static CommandMapper<IPlatformChannelView, PlatformChannelViewHandler> PlatformChannelViewCommandMapper = new(ViewCommandMapper)
	{
	};

	public PlatformChannelViewHandler() : base(PlatformChannelViewViewMapper, PlatformChannelViewCommandMapper)
	{
	}

	public PlatformChannelViewHandler(PropertyMapper propertyMapper, CommandMapper commandMapper) : base(propertyMapper ?? PlatformChannelViewViewMapper, commandMapper ?? PlatformChannelViewCommandMapper)
	{
	}

	public event ChannelMessageDelegate OnReceivedFromPlatform;
}
