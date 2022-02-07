package com.microsoft.maui.platform.channels.sample;

import com.microsoft.maui.platform.channels.Channel;
import com.microsoft.maui.platform.channels.ChannelService;

import java.util.ArrayList;
import java.util.List;

public class MathChannel extends Channel {

    @Override
    public Object handleMessageFromMaui(String messageId, Object... parameters) {
        if (messageId.equals("add"))
        {
            return add(parameters);
        }

        return null;
    }


    Double add(Object... numbers)
    {
        Double v = 0d;

        for (int i = 0; i < numbers.length; i++) {
            Object obj = numbers[i];

            if (obj instanceof Double) {
                v = v.doubleValue() + ((Double) obj).doubleValue();
            }
        }

        return v;
    }
}
