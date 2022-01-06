package com.microsoft.maui.platform.channels;

public interface MessageHandler {
    public default Object onMessage(String messageId, Object... parameters) {
        return null;
    }
}
