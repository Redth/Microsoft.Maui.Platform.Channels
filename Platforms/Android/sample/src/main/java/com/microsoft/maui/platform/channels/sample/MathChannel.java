package com.microsoft.maui.platform.channels.sample;

import com.microsoft.maui.platform.channels.Channel;
import com.microsoft.maui.platform.channels.ChannelService;

public class MathChannel extends Channel {

    @Override
    public Object handleMessageFromMaui(String messageId, Object... parameters) {
        if (messageId.equals("add") && parameters instanceof Number[])
        {
            return add((Number[])parameters);
        }

        return null;
    }


    Number add(Number... numbers)
    {
        Number v = 0;

        for (int i = 0; i < numbers.length; i++) {
            v = v.doubleValue() + numbers[i].doubleValue();
        }


        return v;
    }
}
