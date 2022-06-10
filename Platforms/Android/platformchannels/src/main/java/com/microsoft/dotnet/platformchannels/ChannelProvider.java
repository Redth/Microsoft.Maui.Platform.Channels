package com.microsoft.dotnet.platformchannels;

public interface ChannelProvider {
    public String getDefaultInstanceId();

    public Channel getInstance(String channelId);

    public Channel getInstance(String channelId, String instanceId);
}
