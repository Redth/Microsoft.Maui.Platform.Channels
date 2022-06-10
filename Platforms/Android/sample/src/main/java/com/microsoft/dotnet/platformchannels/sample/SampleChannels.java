package com.microsoft.dotnet.platformchannels.sample;

import com.microsoft.dotnet.platformchannels.ChannelService;

public class SampleChannels {
    public static void init() {
        ChannelService.register("math", MathChannel.class);
    }
}
