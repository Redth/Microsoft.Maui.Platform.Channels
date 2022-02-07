using Microsoft.PlatformChannels;

namespace SamplePlatformChannels;
public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        var channelService = this.Handler.MauiContext.Services.GetRequiredService<IChannelService>();

        var mathChannel = channelService.GetOrCreateChannel("math");

        
        if (double.TryParse(mathChannel.SendToPlatform("add", new[] { 5.0d, 3.0d }).ToString(), out var dbl))
            Console.WriteLine("math -> add: " + dbl);
    }

}