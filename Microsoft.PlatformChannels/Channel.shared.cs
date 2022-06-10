
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
        {
            var platformParams = new List<PlatformObject>(parameters.Length);

            foreach (var p in parameters)
            {
                var platformParam = ToPlatformObject(p);
                platformParams.Add(platformParam);
            }

            var result = PlatformChannel.HandleMessageFromDotNet(messageId, platformParams.ToArray());
            return ToDotNetObject(result);
        }
    }
}