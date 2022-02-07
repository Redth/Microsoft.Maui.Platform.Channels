
using System;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;

namespace Microsoft.PlatformChannels
{
    public partial class ChannelService
    {
        const string LIBOBJC_DYLIB = "/usr/lib/libobjc.dylib";
        [DllImport(LIBOBJC_DYLIB, EntryPoint = "objc_msgSend")]
        internal extern static void void_objc_msgSend(IntPtr receiver, IntPtr selector);

        internal void Initialize()
        {
            var c = new ObjCRuntime.Class(Configuration.InitClassName);
            var s = new ObjCRuntime.Selector(Configuration.InitMethodName);
            void_objc_msgSend(c.Handle, s.Handle);
        }
    }
}