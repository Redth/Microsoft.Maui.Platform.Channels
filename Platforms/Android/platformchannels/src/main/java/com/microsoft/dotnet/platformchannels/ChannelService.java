package com.microsoft.dotnet.platformchannels;


import java.util.Collection;
import java.util.concurrent.ConcurrentHashMap;

public class ChannelService {

    static ChannelService instance;

    private static ChannelService getInstance() {
        if (instance == null)
            instance = new ChannelService();

        return instance;
    }

    ChannelProvider channelProvider;

    public static void setProvider(ChannelProvider provider)
    {
        getInstance().channelProvider = provider;
    }

    public static ChannelProvider getProvider()
    {
        return getInstance().channelProvider;
    }

    ConcurrentHashMap<String, Class<? extends Channel>> channelClasses = new ConcurrentHashMap<>();

    public static void register(String channelId, Class<? extends Channel> channel)
    {
        // Registers a type that can be used to create a new instance of a given channel id
        getInstance().channelClasses.put(channelId, channel);
    }

    public static Channel getOrCreateChannel(String channelId)
    {
        final ChannelProvider provider = getInstance().channelProvider;
        return provider.getInstance(channelId, provider.getDefaultInstanceId());
    }

    public static Channel getOrCreateChannel(String channelId, String instanceId)
    {
        return getInstance().channelProvider.getInstance(channelId, instanceId);
    }

    public static Channel create(String channelId)
    {
        final ChannelService instance = getInstance();

        if (instance.channelClasses.containsKey(channelId))  {
            Class<? extends Channel> channelClass = instance.channelClasses.get(channelId);

            try {
                return channelClass.newInstance();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }

        return null;
    }
}
