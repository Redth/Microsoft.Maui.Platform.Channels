package com.microsoft.maui.platform.channels;

public abstract class Channel {

    public Channel(String channelId)
    {
        this.broker = ChannelBroker.getInstance();
        this.channelId = channelId;
    }

    public Channel(ChannelBroker broker, String channelId)
    {
        this.broker = broker;
        this.channelId = channelId;
    }

    ChannelBroker broker;
    String channelId;

    public String getChannelId()
    {
        return channelId;
    }

    public abstract Object receiveFromManaged(String messageId, Object... parameters);

    public Object sendToManaged(String messageId, Object... parameters) throws Exception {
        return managedHandler.onMessage(messageId, parameters);
    }

    MessageHandler managedHandler;

    public void setManagedHandler(MessageHandler handler) {
        managedHandler = handler;
    }

}

