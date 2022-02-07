//using System;
//using System.Runtime.CompilerServices;
//using Microsoft.Maui.Handlers;
//using Microsoft.Maui.Platform.Channels;
//using AndroidChannelBroker = Microsoft.Maui.Platform.Channels.Android.Channels;
//using AndroidViewChannel = Microsoft.Maui.Platform.Channels.Android.ViewChannel;
//using Channel = System.Threading.Channels.Channel;

//namespace SamplePlatformChannels
//{
//    public partial class PlatformChannelViewHandler : ViewHandler<IPlatformChannelView, global::Android.Views.View>
//    {
//        private AndroidViewChannel platformViewChannel;
        
//        protected override global::Android.Views.View CreateNativeView()
//        {
//            platformViewChannel = AndroidChannelBroker.Instance.CreateViewChannel(this.VirtualView.ChannelTypeId, 
//                new PlatformMessageHandler(VirtualView.ReceiveFromPlatform));

//            return platformViewChannel.CreatePlatformView(MauiContext.Context);
//        }
        
//        Java.Lang.Object SendToPlatform(string messageId, object[] args)
//        {
//            if (args.Length <= 0)
//                throw new ArgumentNullException("id");

//            if (args[0] is not string id)
//                throw new ArgumentException("id");

//            Java.Lang.Object[] p = null;

//            if (args.Length > 1)
//                p = args.Skip(1).ToArray().Select(a => a as Java.Lang.Object).ToArray();

//            return platformViewChannel?.ReceiveFromManaged(id, p);
//        }
//    }
//}

