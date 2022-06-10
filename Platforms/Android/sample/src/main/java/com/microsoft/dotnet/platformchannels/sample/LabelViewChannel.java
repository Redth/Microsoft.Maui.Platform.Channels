package com.microsoft.dotnet.platformchannels.sample;

import android.content.Context;
import android.view.View;
import android.widget.TextView;

import androidx.core.widget.TextViewCompat;

import com.microsoft.dotnet.platformchannels.Channel;
import com.microsoft.dotnet.platformchannels.ChannelService;
import com.microsoft.dotnet.platformchannels.ViewChannel;

import java.util.ArrayList;
import java.util.List;

public class LabelViewChannel extends ViewChannel {
    @Override
    public Object handleMessageFromDotNet(String messageId, Object... parameters) {
        if (messageId.equals("setText"))
        {
            textView.setText((String)parameters[0]);
            return textView.getText();
        }

        return null;
    }

    TextView textView;

    @Override
    public View createPlatformView(Context context) {
        if (textView == null)
            textView = new TextView(context);
        return textView;
    }
}
