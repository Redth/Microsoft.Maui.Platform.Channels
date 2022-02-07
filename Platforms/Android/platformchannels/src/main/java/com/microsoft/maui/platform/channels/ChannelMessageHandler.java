package com.microsoft.maui.platform.channels;

public interface ChannelMessageHandler {
    public Object onChannelMessage(String messageId, Object... parameters);
}
