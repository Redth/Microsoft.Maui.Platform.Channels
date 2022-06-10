using Microsoft.PlatformChannels;

namespace Microsoft.Maui.PlatformChannels;

public partial class PlatformChannelView : View, IPlatformChannelView
{
	public string ChannelTypeId { get; }

	public event ChannelMessageDelegate OnReceiveFromPlatform
	{
		add
		{
			if (Handler is PlatformChannelViewHandler p)
				p.OnReceivedFromPlatform += value; 
		}
		remove
		{
			if (Handler is PlatformChannelViewHandler p)
				p.OnReceivedFromPlatform -= value;
		}
	}

	public object SendToPlatform(string id, params object[] parameters)
		=> (Handler as PlatformChannelViewHandler)?.SendToPlatformImpl(id, parameters);

}
