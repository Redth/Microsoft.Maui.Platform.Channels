using Microsoft.Maui.Handlers;
using Microsoft.PlatformChannels;
using Foundation;
using UIKit;

using iOSViewChannel = Microsoft.PlatformChannels.Platform.ViewChannel;

namespace Microsoft.Maui.PlatformChannels;

public partial class PlatformChannelViewHandler : ViewHandler<IPlatformChannelView, UIKit.UIView>
{
	iOSViewChannel platformViewChannel;
	PlatformManagedHandler managedHandler;
	UIView viewGroup;

	protected string ChannelTypeId { get; private set; }
	protected string ChannelInstanceId { get; private set; }

	IChannelService channelService;

	protected override UIKit.UIView CreatePlatformView()
	{
		if (viewGroup is null) {
			// TODO - Fix this hard coded size
			viewGroup = new UIView(new CoreGraphics.CGRect(0, 0, 400, 100));
		}
		EnsureChannelCreated();
		return viewGroup;
	}

	void EnsureChannelCreated()
	{
		if (string.IsNullOrEmpty(VirtualView?.ChannelTypeId))
			return;

		var instanceId = VirtualView?.ChannelInstanceId ?? ChannelService.DEFAULT_INSTANCE_ID;

		// Is the new id the same as the old?
		if (VirtualView?.ChannelTypeId == ChannelTypeId)
		{
			// Is the instance null on both (so both default and the same)
			// or is the new instance Id not the same as the old
			if (instanceId == ChannelInstanceId)
				return; // No change to actual channel, return
		}

		channelService ??= MauiContext.Services.GetRequiredService<IChannelService>();

		if (platformViewChannel is not null)
		{
			foreach (var subview in viewGroup.Subviews) {
				subview.RemoveFromSuperview ();
			}

			if (viewGroup.Superview is not null)
				viewGroup.RemoveFromSuperview ();

			channelService.DisposeChannel(ChannelTypeId, ChannelInstanceId);
			platformViewChannel = null;
		}

		ChannelTypeId = VirtualView.ChannelTypeId;
		ChannelInstanceId = instanceId;

		var channel = Microsoft.PlatformChannels.Platform.ChannelService.GetOrCreate(ChannelTypeId, ChannelInstanceId);

		if (channel == null) {
			throw new InvalidOperationException($"No registered ViewChannel found for: '{ChannelTypeId}', instance: '{ChannelInstanceId}'");
		}

		if (channel is not iOSViewChannel viewChannel)
			throw new InvalidCastException($"Registered channel '{ChannelTypeId}' is not a 'ViewChannel' type for instance: '{ChannelInstanceId}");

		platformViewChannel = viewChannel;

		managedHandler = new PlatformManagedHandler((id, parameters) =>
		{
			return OnReceivedFromPlatform?.Invoke(id, parameters);
		});

		platformViewChannel.SetManagedHandler(managedHandler);

		var platformView = platformViewChannel.GetPlatformView();
		viewGroup.AddSubview (platformView);
	}
	
    public static void MapChannelTypeId(IPlatformViewHandler handler, IPlatformChannelView view)
	{
		if (handler is PlatformChannelViewHandler h)
			h.EnsureChannelCreated();
	}

	public static void MapChannelInstanceId(IPlatformViewHandler handler, IPlatformChannelView view)
	{
		if (handler is PlatformChannelViewHandler h)
			h.EnsureChannelCreated();
	}

    internal object SendToPlatformImpl(string messageId, object[] args)
    {
		var platformObjs = args.ToPlatformObjects();

		var platformResp = platformViewChannel?.OnChannelMessage(messageId, platformObjs);

		var result = platformResp.ToDotNetObject();

		return result;
    }

	class PlatformManagedHandler : NSObject, Microsoft.PlatformChannels.Platform.IChannelMessageHandler
	{
		public PlatformManagedHandler(Func<string, object[], object> callback)
		{
			Callback = callback;
		}

		protected readonly Func<string, object[], object> Callback;

		public PlatformObject OnChannelMessage(string id, PlatformObject[] parameters)
			=> Callback?.Invoke(id, parameters.ToDotNetObjects()).ToPlatformObject();
	}
}