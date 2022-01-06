package com.microsoft.maui.platform.channels;

import android.view.View;

import java.util.concurrent.Callable;
import java.util.concurrent.ConcurrentHashMap;

public class ChannelBroker {

    static ChannelBroker instance;

    public static ChannelBroker getInstance() {
        if (instance == null)
            instance = new ChannelBroker();

        return instance;
    }


    ConcurrentHashMap<String, Callable<Channel>> channelFactories = new ConcurrentHashMap<>();

    // Call from Java
    public void registerFactory(String channelTypeId, Callable<Channel> factory)
    {
        channelFactories.put(channelTypeId, factory);
    }

    // Call from .NET
    public Channel createChannel(String channelTypeId, MessageHandler messageHandler) throws Exception
    {
        Callable<Channel> channelFactory = channelFactories.get(channelTypeId);

        Channel channel = channelFactory.call();

        channel.setManagedHandler(messageHandler);

        return channel;
    }
}
