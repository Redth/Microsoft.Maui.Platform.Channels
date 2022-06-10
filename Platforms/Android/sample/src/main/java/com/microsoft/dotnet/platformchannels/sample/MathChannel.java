package com.microsoft.dotnet.platformchannels.sample;

import android.view.View;

import com.microsoft.dotnet.platformchannels.Channel;
import com.microsoft.dotnet.platformchannels.ChannelService;

import java.util.ArrayList;
import java.util.List;

public class MathChannel extends Channel {

    @Override
    public Object handleMessageFromDotNet(String messageId, Object... parameters) {
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
