package com.microsoft.maui.platform.channels.internal;

import com.microsoft.maui.platform.channels.MessageHandler;

import java.util.concurrent.ConcurrentHashMap;

public class InternalMessaging {
    static MessageHandler managedHandler = null;
    static ConcurrentHashMap<String, MessageHandler> platformHandlers = new ConcurrentHashMap<String, MessageHandler>();

    public static <TReturn extends Object, TParam extends Object> TReturn SendToPlatform(String id, TParam... parameters) throws Exception
    {
        if (platformHandlers.containsKey(id))
        {
            final MessageHandler handler = platformHandlers.get(id);
            final Object result = handler.onMessageReceived(id, parameters);
            return (TReturn)result;
        }
        return null;
    }

    public static <TReturn extends Object, TParam extends Object> TReturn SendFromPlatform(String id, TParam... parameters) throws Exception
    {
        if (managedHandler != null)
        {
            final Object result = managedHandler.onMessageReceived(id, parameters);
            return (TReturn)result;
        }
        return null;
    }

    public static void setManagedHandler(MessageHandler handler)
    {
        managedHandler = handler;
    }

    public static void registerMessageHandler(String id, MessageHandler handler)
    {
        platformHandlers.put(id, handler);
    }

    public static void unregisterMessageHandler(String id, MessageHandler handler)
    {
        platformHandlers.remove(id);
    }
}
