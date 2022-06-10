package com.microsoft.dotnet.platformchannels;

import android.content.Context;
import android.view.View;

import java.util.UUID;

public abstract class ViewChannel extends Channel {

    View platformView;

    public View getView() {
        return platformView;
    }

    public abstract View createPlatformView(Context context);
}
