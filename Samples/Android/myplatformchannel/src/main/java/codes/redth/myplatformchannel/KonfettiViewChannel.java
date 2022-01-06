package codes.redth.myplatformchannel;

import android.content.Context;
import android.graphics.Color;
import android.view.View;

import com.microsoft.maui.platform.channels.ChannelBroker;
import com.microsoft.maui.platform.channels.ViewChannel;

import nl.dionsegijn.konfetti.KonfettiView;
import nl.dionsegijn.konfetti.models.Shape;
import nl.dionsegijn.konfetti.models.Size;

public class KonfettiViewChannel extends ViewChannel {

    public KonfettiViewChannel(ChannelBroker broker) {
        super(broker);
    }

    KonfettiView konfettiView;

    @Override
    public View createPlatformView(Context context) {
        konfettiView = new KonfettiView(context);
        return konfettiView;
    }

    @Override
    public Object receiveFromManaged(String id, Object... objects) {

        Integer sum = 0;

        for (int i = 0; i < objects.length; i++)
        {
            if (objects[i] instanceof Integer)
            {
                sum += (Integer) objects[i];
            }
        }
        if (id.equals("start")) {
            // Stop existing animation first
            if (konfettiView.isActive()) {
                konfettiView.stopGracefully();
                konfettiView.isActive();
            }

            // Show new konfetti
            konfettiView.build()
                    .addColors(Color.YELLOW, Color.GREEN, Color.MAGENTA)
                    .setDirection(0.0, 359.0)
                    .setSpeed(1f, 5f)
                    .setFadeOutEnabled(true)
                    .setTimeToLive(2000L)
                    .addShapes(Shape.Square.INSTANCE, Shape.Circle.INSTANCE)
                    .addSizes(new Size(12, 5f))
                    //.setPosition(-50f, kf.getWidth() + 50f, -50f, -50f)
                    .streamFor(300, 5000L);
        } else if (id.equals("stop")) {
            // Stop nicely
            konfettiView.stopGracefully();
        }
        return null;
    }
}
