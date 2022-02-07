package com.microsoft.maui.platform.channels;

public abstract class Channel implements AutoCloseable {

    public Object sendMessageToMaui(String messageId, Object... parameters) throws Exception {

        if (managedHandler == null)
            throw new Exception("No MAUI Runtime registration for this Channel");

        return managedHandler.onChannelMessage(messageId, parameters);
    }

    public abstract Object handleMessageFromMaui(String messageId, Object... parameters);

    ChannelMessageHandler managedHandler;

    public void setManagedHandler(ChannelMessageHandler handler) {
        managedHandler = handler;
    }


    @Override
    public void close() throws Exception {
        managedHandler = null;
    }
}

