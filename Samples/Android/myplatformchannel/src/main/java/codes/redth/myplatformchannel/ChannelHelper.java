package codes.redth.myplatformchannel;

import android.graphics.Color;

import com.microsoft.maui.platform.channels.*;
import nl.dionsegijn.konfetti.KonfettiView;
import nl.dionsegijn.konfetti.models.Shape;
import nl.dionsegijn.konfetti.models.Size;

public class ChannelHelper {

    public static void Init()
    {
        PlatformChannel.registerMessageHandler("createConfettiView", new MessageHandler() {
            @Override
            public Object onMessageReceived(String id, Object... parameters) {
                KonfettiView kf = new KonfettiView(null);
                kf.build().addColors(Color.YELLOW, Color.GREEN, Color.MAGENTA)
                        .setDirection(0.0, 359.0)
                        .setSpeed(1f, 5f)
                        .setFadeOutEnabled(true)
                        .setTimeToLive(2000L)
                        .addShapes(Shape.Square.INSTANCE, Shape.Circle.INSTANCE)
                        .addSizes(new Size(12, 5f))
                        .setPosition(-50f, kf.getWidth() + 50f, -50f, -50f)
                        .streamFor(300, 5000L);
                return kf;
            }
        });
    }
}
