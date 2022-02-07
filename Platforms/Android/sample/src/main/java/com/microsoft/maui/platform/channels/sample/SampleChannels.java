package com.microsoft.maui.platform.channels.sample;

import com.microsoft.maui.platform.channels.ChannelService;

public class SampleChannels {
    public static void init() {
        ChannelService.register("math", MathChannel.class);
    }
}
