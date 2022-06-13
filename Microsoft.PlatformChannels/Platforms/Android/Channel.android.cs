
using Android.Content;

namespace Microsoft.PlatformChannels;

public partial class ViewChannel
{
	public Android.Content.Context Context { get; set; }

	public virtual PlatformView GetPlatformView()
		=> PlatformViewChannel?.GetPlatformView(Context);
}
