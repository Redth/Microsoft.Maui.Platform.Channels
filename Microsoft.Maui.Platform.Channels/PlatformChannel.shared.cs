namespace Microsoft.Maui.Platform.Channels;

public static partial class PlatformChannel
{
    static readonly Dictionary<string, MessageHandlerDelegate> handlers = new();

    public static void Register(string id, Func<string, object[], object> handler)
        => Register(id, handler);

    public static void Register(string id, MessageHandlerDelegate handler)
        => handlers[id] = handler;

    public static void Unregister(string id)
    {
        if (handlers.ContainsKey(id))
            handlers.Remove(id);
    }

    public static void UnregisterAll()
        => handlers.Clear();
}
