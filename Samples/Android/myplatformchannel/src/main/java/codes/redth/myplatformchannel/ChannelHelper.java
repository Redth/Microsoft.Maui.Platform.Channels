package codes.redth.myplatformchannel;

import com.microsoft.maui.platform.channels.*;

public class ChannelHelper {

    static ChannelBroker channelBroker;

    public static void Init() throws Exception
    {
        channelBroker = ChannelBroker.getInstance();
        channelBroker.registerFactory("konfettiview", () -> new KonfettiViewChannel(channelBroker));
    }
}
