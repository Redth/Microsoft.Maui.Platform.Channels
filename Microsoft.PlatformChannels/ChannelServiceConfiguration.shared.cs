namespace Microsoft.PlatformChannels
{
    public partial class ChannelServiceConfiguration
    {
        public ChannelServiceConfiguration()
        {
        }

        public ChannelServiceConfiguration(string androidInitPackageName, string androidInitClassName, string androidInitMethodName = "init")
        {
            InitPackageName = androidInitPackageName;
            InitClassName = androidInitClassName;
            InitMethodName = androidInitMethodName;
        }

        public ChannelServiceConfiguration(string appleInitClassName, string appleInitMethodName = "init")
        {
            InitPackageName = string.Empty;
            InitClassName = appleInitClassName;
            InitMethodName = appleInitMethodName;
        }

        public readonly string InitPackageName;
        public readonly string InitClassName;
        public readonly string InitMethodName;
    }

    public delegate object ChannelMessageDelegate(string messageId, params object[] parameters);

}