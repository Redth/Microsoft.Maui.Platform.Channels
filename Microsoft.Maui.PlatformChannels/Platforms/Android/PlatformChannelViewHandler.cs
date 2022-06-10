using System;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Handlers;

using AndroidViewChannel = Microsoft.PlatformChannels.Platform.ViewChannel;
using PChannel = Microsoft.PlatformChannels.Channel;

namespace Microsoft.Maui.PlatformChannels;

public partial class PlatformChannelViewHandler : ViewHandler<IPlatformChannelView, global::Android.Views.View>
{
	AndroidViewChannel platformViewChannel;
	PlatformManagedHandler managedHandler;

	protected override global::Android.Views.View CreatePlatformView()
	{
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

		return platformViewChannel.CreatePlatformView(MauiContext.Context);
	}

	internal object SendToPlatformImpl(string messageId, object[] args)
		=> PChannel.ToDotNetObject(platformViewChannel?.HandleMessageFromDotNet(messageId, PChannel.ToPlatformObjects(args)));	

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



