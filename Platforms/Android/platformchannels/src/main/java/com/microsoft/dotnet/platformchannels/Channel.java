package com.microsoft.dotnet.platformchannels;

public abstract class Channel implements AutoCloseable {

    public Object sendMessageToDotNet(String messageId, Object[] args) throws Exception {

        if (managedHandler == null)
            throw new Exception("No MAUI Runtime registration for this Channel");

        return managedHandler.onChannelMessage(messageId, args);
    }

    public abstract Object handleMessageFromDotNet(String messageId, Object[] args);

    ChannelMessageHandler managedHandler;

    public void setManagedHandler(ChannelMessageHandler handler) {
        managedHandler = handler;
    }


    @Override
    public void close() throws Exception {
        managedHandler = null;
    }
}

