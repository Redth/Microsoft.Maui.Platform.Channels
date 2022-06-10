
namespace Microsoft.PlatformChannels
{
    public partial class Channel : PlatformObject, IPlatformChannelMessageHandler
    {
        internal Channel(PlatformChannel platformChannel)
        {
            PlatformChannel = platformChannel;
            PlatformChannel.SetManagedHandler(this);
        }

        internal PlatformChannel PlatformChannel { get; set; }

        public PlatformObject OnChannelMessage(string messageId, params PlatformObject[] parameters)
            => ToPlatformObject(HandleFromPlatform(messageId, ToDotNetObjects(parameters)));

        public virtual object HandleFromPlatform(string messageId, params object[] parameters)
            => null;

        public object SendToPlatform(string messageId, params object[] parameters)
            => ToDotNetObject(PlatformChannel.HandleMessageFromDotNet(messageId, ToPlatformObjects(parameters)));
    }
}