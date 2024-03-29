﻿using PlatformChannel = Microsoft.PlatformChannels.Platform.Channel;
using IPlatformChannelProvider = Microsoft.PlatformChannels.Platform.IChannelProvider;
using PlatformChannelService = Microsoft.PlatformChannels.Platform.ChannelService;

#if IOS || MACCATALYST
using PlatformObject = Foundation.NSObject;
#elif ANDROID
using PlatformObject = Java.Lang.Object;
#endif

namespace Microsoft.PlatformChannels;

internal class ChannelInstanceHolder
{
	public Channel Instance { get; set; }
	public PlatformChannel PlatformInstance { get; set; }
	public string ChannelId { get; set; }
	public string InstanceId { get; set; }
}

public interface IManagedChannelProvider
{
	public Channel GetManagedInstance(string channelId, string instanceId);
}

public partial class ChannelProvider
	: PlatformObject, IPlatformChannelProvider, IManagedChannelProvider
{
	public string DefaultInstanceId => ChannelService.DEFAULT_INSTANCE_ID;

	Dictionary<string, Dictionary<string, ChannelInstanceHolder>> channels = new();

	ChannelInstanceHolder GetChannelInstanceHolder(string channelId, string instanceId)
	{
		if (!channels.ContainsKey(channelId))
			channels[channelId] = new();

		var instances = channels[channelId];

		if (string.IsNullOrEmpty(instanceId))
			instanceId = DefaultInstanceId;

		if (!instances.ContainsKey(instanceId))
		{
			var platformInstance = PlatformChannelService.Create(channelId);

			instances[instanceId] = new ChannelInstanceHolder
			{
				ChannelId = channelId,
				InstanceId = instanceId,
				Instance = new Channel(platformInstance),
				PlatformInstance = platformInstance
			};
		}

		return instances[instanceId];
	}

	public PlatformChannel GetInstance(string channelId)
		=> GetInstance(channelId, DefaultInstanceId);

	public PlatformChannel GetInstance(string channelId, string instanceId)
		=> GetChannelInstanceHolder(channelId, instanceId).PlatformInstance;

	public Channel GetManagedInstance(string channelId, string instanceId)
		=> GetChannelInstanceHolder(channelId, instanceId).Instance;

	public void DisposeInstance(string channelId, string instanceId)
	{
		if (!channels.ContainsKey(channelId))
			return;

		var instances = channels[channelId];

		if (string.IsNullOrEmpty(instanceId))
			instanceId = DefaultInstanceId;

		if (!instances.ContainsKey(instanceId))
			return;

		var instance = instances[instanceId];
		instances.Remove(instanceId);

#if ANDROID
		instance.PlatformInstance.Close();
#endif
		instance.PlatformInstance = null;
		instance.Instance = null;
	}
}