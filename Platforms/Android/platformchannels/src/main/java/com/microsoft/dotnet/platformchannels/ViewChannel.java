package com.microsoft.dotnet.platformchannels;

import android.content.Context;
import android.view.View;

import java.util.UUID;

public abstract class ViewChannel extends Channel {
    public abstract View getPlatformView(Context context);
}
