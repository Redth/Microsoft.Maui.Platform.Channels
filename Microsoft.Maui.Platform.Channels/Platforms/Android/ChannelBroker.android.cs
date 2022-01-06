
using System;
using System.Linq;
using Android.Runtime;
using AndroidChannelBroker = global::Microsoft.Maui.Platform.Channels.Android.ChannelBroker;
using AndroidMessageHandler = Microsoft.Maui.Platform.Channels.Android.IMessageHandler;

namespace Microsoft.Maui.Platform.Channels
{
    public partial class ChannelBroker
    {
        void Init()
        {
            if (!string.IsNullOrEmpty(Configuration.JavaInitPackageName) &&
                !string.IsNullOrEmpty(Configuration.JavaInitMethodName))
            {
                // Call the java side init which gives the java library a chance to register for its own handlers on the platform side
                var javaStartClassPath =
                    string.Join(".", Configuration.JavaInitPackageName, Configuration.JavaInitClassName);
                javaStartClassPath = javaStartClassPath.Replace('.', '/');
                var startClass = JNIEnv.FindClass(javaStartClassPath);
                var method = JNIEnv.GetStaticMethodID(
                    startClass,
                    Configuration.JavaInitMethodName,
                    "(Landroid/content/Context;)V");
                JNIEnv.CallStaticVoidMethod(startClass, method);
            }
        }

        public ViewChannel CreateViewChannel(string channelTypeId, ChannelMessageDelegate messageHandler)
            => CreateChannel(channelTypeId, messageHandler) as ViewChannel; 

        public Channel CreateChannel(string channelTypeId, ChannelMessageDelegate messageHandler)
        {
            var platformChannel = AndroidChannelBroker.Instance.CreateChannel(
                channelTypeId,
                new PlatformMessageHandler((id, p) => messageHandler(id, p)));

            return new Channel(platformChannel.Id, messageHandler) {PlatformChannel = platformChannel};
        }

        
    }
    
    class PlatformMessageHandler : Java.Lang.Object, AndroidMessageHandler
    {
        public PlatformMessageHandler(Func<string, object[], object> handler)
            => Handler = handler;

        protected readonly Func<string, object[], object> Handler;

        public object OnMessageReceived(string messageId, params Java.Lang.Object[] parameters)
            => Handler?.Invoke(messageId, parameters);
    }
}