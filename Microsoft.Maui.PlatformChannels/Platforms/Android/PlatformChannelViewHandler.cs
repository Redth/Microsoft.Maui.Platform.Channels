using System;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Handlers;
using Microsoft.PlatformChannels;
using AndroidViewChannel = Microsoft.PlatformChannels.Platform.ViewChannel;
using PChannel = Microsoft.PlatformChannels.Channel;
using AViewGroup = Android.Widget.LinearLayout;

namespace Microsoft.Maui.PlatformChannels;

public partial class PlatformChannelViewHandler : ViewHandler<IPlatformChannelView, global::Android.Views.View>
{
	AndroidViewChannel platformViewChannel;
	PlatformManagedHandler managedHandler;
	AViewGroup viewGroup;

	protected string ChannelTypeId { get; private set; }
	protected string ChannelInstanceId { get; private set; }

	IChannelService channelService;

	protected override global::Android.Views.View CreatePlatformView()
	{
		if (viewGroup is null)
			viewGroup = new AViewGroup(Context);
		EnsureChannelCreated();
		
		return viewGroup;
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
			viewGroup.RemoveAllViews();

			if (viewGroup.Parent is Android.Views.ViewGroup vg)
				vg.RemoveView(viewGroup);

			channelService.DisposeChannel(ChannelTypeId, ChannelInstanceId);
			platformViewChannel = null;
		}

		ChannelTypeId = VirtualView.ChannelTypeId;
		ChannelInstanceId = instanceId;


		var channel = Microsoft.PlatformChannels.Platform.ChannelService.GetOrCreateChannel(ChannelTypeId, ChannelInstanceId);

		if (channel == null)
			throw new InvalidOperationException($"No registered ViewChannel found for: '{ChannelTypeId}', instance: '{ChannelInstanceId}'");

		if (channel is not AndroidViewChannel viewChannel)
			throw new InvalidCastException($"Registered channel '{ChannelTypeId}' is not a 'ViewChannel' type for instance: '{ChannelInstanceId}");

		platformViewChannel = viewChannel;

		managedHandler = new PlatformManagedHandler((id, parameters) =>
		{
			return OnReceivedFromPlatform?.Invoke(id, parameters);
		});

		platformViewChannel.SetManagedHandler(managedHandler);

		var platformView = platformViewChannel.GetPlatformView(Context);
		viewGroup.AddView(platformView);
	}

	internal object SendToPlatformImpl(string messageId, object[] args)
	{
		var platformObjs = PChannel.ToPlatformObjects(args);

		var platformResp = platformViewChannel?.HandleMessageFromDotNet(messageId, platformObjs);

		var result = PChannel.ToDotNetObject(platformResp);

		return result;
	}


	class PlatformManagedHandler : Java.Lang.Object, Microsoft.PlatformChannels.Platform.IChannelMessageHandler
	{
		public PlatformManagedHandler(Func<string, object[], object> callback)
		{
			Callback = callback;
		}

		protected readonly Func<string, object[], object> Callback;

		public PlatformObject OnChannelMessage(string id, PlatformObject[] parameters)
			=> PChannel.ToPlatformObject(Callback?.Invoke(id, PChannel.ToDotNetObjects(parameters)));
	}
}



