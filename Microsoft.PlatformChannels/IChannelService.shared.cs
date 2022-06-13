namespace Microsoft.PlatformChannels;

public interface IChannelService
{
	ChannelServiceConfiguration Configuration { get; }

	Channel GetOrCreateChannel(string channelId, string instanceId = null);

	void DisposeChannel(string channelId, string instanceId = null);
}