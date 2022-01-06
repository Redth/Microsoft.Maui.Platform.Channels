package com.microsoft.maui.platform.channels;

import android.content.Context;
import android.view.View;

import java.util.UUID;

public abstract class ViewChannel extends Channel {

    View platformView;

    public ViewChannel() {
        super(UUID.randomUUID().toString());
    }
    public ViewChannel(ChannelBroker broker) {
        super(broker, UUID.randomUUID().toString());
    }

    public ViewChannel(String channelId) {
        super(channelId);
    }

    public ViewChannel(ChannelBroker broker, String channelId) {
        super(broker, channelId);
    }

    public View getPlatformView() {
        return platformView;
    }

    public abstract View createPlatformView(Context context);
}
