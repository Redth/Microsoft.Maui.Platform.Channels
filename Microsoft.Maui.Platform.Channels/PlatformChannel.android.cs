
using Android.Runtime;
using Microsoft.Maui.Platform.Channels.Android;

namespace Microsoft.Maui.Platform.Channels;

public static partial class PlatformChannel
{
    static IMessageHandler fromPlatformHandler;

    public static void Init(global::Android.Content.Context context, string javaPackageName, string javaStartClassName, string javaStartMethodName = "init")
    {
        fromPlatformHandler = new FromPlatformMessageHandler((id, p) => handlers?[id]?.Invoke(id, p));

        // Setup our handler so the platform side has a hook to callback for any messages it needs to send
        Android.Internal.InternalMessaging.SetManagedHandler(fromPlatformHandler);

        // Call the java side init which gives the java library a chance to register for its own handlers on the platform side
        var javaStartClassPath = string.Join(".", javaPackageName, javaStartClassName);
        javaStartClassPath = javaStartClassPath.Replace('.', '/');
        var startClass = JNIEnv.FindClass(javaStartClassPath);
        var method = JNIEnv.GetStaticMethodID(
            startClass,
            javaStartMethodName,
            "(Landroid/content/Context;)V");
        JNIEnv.CallStaticVoidMethod(startClass, method, new JValue(context));
    }
   

    public delegate Java.Lang.Object MessageHandlerDelegate(string id, params Java.Lang.Object[] parameters);


    public static object SendToPlatform(string id, object[] parameters)
        => SendToPlatform(id, parameters.Select(p => p as Java.Lang.Object).ToArray());

    public static Java.Lang.Object SendToPlatform(string id, params Java.Lang.Object[] parameters)
        => Android.Internal.InternalMessaging.SendToPlatform(id, parameters);

    class FromPlatformMessageHandler : Java.Lang.Object, IMessageHandler
    {
        public FromPlatformMessageHandler(MessageHandlerDelegate handler)
            => Handler = handler;

        protected readonly MessageHandlerDelegate Handler;

        public object OnMessageReceived(string id, params Java.Lang.Object[] parameters)
            => Handler?.Invoke(id, parameters);
    }
}
