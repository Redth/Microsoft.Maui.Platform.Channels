using Microsoft.PlatformChannels;

namespace SamplePlatformChannels;

public partial class App : Application
{
    public App(IChannelService channelService)
    {
        InitializeComponent();

        MainPage = new MainPage(channelService);
    }
}