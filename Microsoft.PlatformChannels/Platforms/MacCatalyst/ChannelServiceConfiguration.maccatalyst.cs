namespace Microsoft.PlatformChannels
{
    public partial class ChannelServiceConfiguration
    {
        public ChannelServiceConfiguration(string className, string methodName = "init")
        {
            InitClassName = className;
            InitMethodName = methodName;
        }

        public readonly string InitClassName;
        public readonly string InitMethodName;
    }
}