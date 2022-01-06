namespace Microsoft.Maui.Platform.Channels;

public partial class ChannelBrokerConfiguration
{
    public ChannelBrokerConfiguration(string javaPackageName, string javaStartClassName, string javaStartMethodName = "init")
    {
        JavaInitPackageName = javaPackageName;
        JavaInitClassName = javaStartClassName;
        JavaInitMethodName = javaStartMethodName;
    }
    
    public readonly string JavaInitPackageName;
    public readonly string JavaInitClassName;
    public readonly string JavaInitMethodName;
}
