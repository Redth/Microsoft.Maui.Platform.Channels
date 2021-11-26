package com.microsoft.maui.platform.channels;

public interface MessageHandler {
    public default Object onMessageReceived(String id, Object... parameters) {
        return null;
    }
}
