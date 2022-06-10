package com.microsoft.dotnet.platformchannels;

public interface ChannelMessageHandler {
    public Object onChannelMessage(String messageId, Object[] parameters);
}
