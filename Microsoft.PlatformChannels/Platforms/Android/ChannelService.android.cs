
using System;
using System.Linq;
using Android.Runtime;

namespace Microsoft.PlatformChannels
{
    public partial class ChannelService
    {
        internal void Initialize()
        {
            if (!string.IsNullOrEmpty(Configuration.InitPackageName) &&
                !string.IsNullOrEmpty(Configuration.InitMethodName))
            {
                // Call the java side init which gives the java library a chance to register for its own handlers on the platform side
                var javaStartClassPath =
                    string.Join(".", Configuration.InitPackageName, Configuration.InitClassName);
                javaStartClassPath = javaStartClassPath.Replace('.', '/');
                var startClass = JNIEnv.FindClass(javaStartClassPath);
                var method = JNIEnv.GetStaticMethodID(
                    startClass,
                    Configuration.InitMethodName,
                    "()V");
                JNIEnv.CallStaticVoidMethod(startClass, method);
            }
        }
    }
}