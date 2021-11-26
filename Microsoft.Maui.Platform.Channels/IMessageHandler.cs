

namespace Microsoft.Maui.Platform.Channels;

public interface IMessageHandler
{
    object OnMessageReceived(string id, params object [] parameters);
}
