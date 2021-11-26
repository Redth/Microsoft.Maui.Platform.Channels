package com.microsoft.maui.platform.channels;

import com.microsoft.maui.platform.channels.internal.InternalMessaging;

public class PlatformChannel {

    public static void registerMessageHandler(String id, MessageHandler handler)
    {
        InternalMessaging.registerMessageHandler(id, handler);
    }

    public static void unregisterMessageHandler(String id, MessageHandler handler)
    {
        InternalMessaging.unregisterMessageHandler(id, handler);
    }
}

