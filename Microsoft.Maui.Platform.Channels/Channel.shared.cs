namespace Microsoft.Maui.Platform.Channels;

public partial class Channel
{
    public Channel(string id, ChannelMessageDelegate messageReceiverDelegate = null)
    {
        receiverDelegate = messageReceiverDelegate;
        
        Id = id;
    }

    public readonly string ChannelTypeId;
    public readonly string Id;

    ChannelMessageDelegate receiverDelegate;

    public virtual object ReceiveFromPlatform(string messageId, params object[] parameters)
        => receiverDelegate?.Invoke(messageId, parameters);

}
