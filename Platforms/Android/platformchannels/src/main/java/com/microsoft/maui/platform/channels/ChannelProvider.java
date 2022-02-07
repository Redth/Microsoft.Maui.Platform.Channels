package com.microsoft.maui.platform.channels;

public interface ChannelProvider {
    public String getDefaultInstanceId();

    public Channel getInstance(String channelId);

    public Channel getInstance(String channelId, String instanceId);
}
