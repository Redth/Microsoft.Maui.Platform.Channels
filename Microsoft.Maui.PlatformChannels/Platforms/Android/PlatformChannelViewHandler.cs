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
	string channelId = null;
	AViewGroup viewGroup;

	protected override global::Android.Views.View CreatePlatformView()
	{
		if (viewGroup is null)
			viewGroup = new AViewGroup(Context);
		EnsureChannelCreated(channelId);
		
		return viewGroup;
	}

	public static void MapChannelTypeId(IPlatformViewHandler handler, IPlatformChannelView view)
	{
		if (handler is PlatformChannelViewHandler h)
			h.EnsureChannelCreated(view.ChannelTypeId);
	}

	void EnsureChannelCreated(string channelId)
	{
		if (string.IsNullOrEmpty(channelId))
			return;

		var channelService = MauiContext.Services.GetRequiredService<IChannelService>();

		if (platformViewChannel is not null)
		{
			// If not a different ID then return, we already created
			if (this.channelId.Equals(channelId, StringComparison.Ordinal))
				return;

			viewGroup.RemoveAllViews();

			platformViewChannel.Close();
			platformViewChannel.Dispose();
			platformViewChannel = null;
		}

		var channel = Microsoft.PlatformChannels.Platform.ChannelService.GetOrCreateChannel(VirtualView.ChannelTypeId);

		if (channel == null)
			throw new InvalidOperationException($"No registered ViewChannel found for: '{VirtualView.ChannelTypeId}'");

		if (channel is not AndroidViewChannel viewChannel)
			throw new InvalidCastException($"Registered channel '{VirtualView.ChannelTypeId}' is not a 'ViewChannel' type.");

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



