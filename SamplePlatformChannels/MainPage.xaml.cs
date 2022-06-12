using Microsoft.PlatformChannels;

namespace SamplePlatformChannels;
public partial class MainPage : ContentPage
{
    
    public MainPage(IChannelService channelService)
    {
        ChannelService = channelService;

        InitializeComponent();
    }

    public readonly IChannelService ChannelService;

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
		labelViewChannel.SendToPlatform("setText", "Hello, From MAUI!");


        if (string.IsNullOrEmpty(entryNumbers?.Text))
            return;

		// Math service, use independently
		
        var mathChannel = ChannelService.GetOrCreateChannel("math");

        var entered = entryNumbers.Text.Split(' ', ',', ';');

        var items = new List<object>();

        foreach (var n in entered)
        {
            if (double.TryParse(n, out var ndbl))
                items.Add(ndbl);
        }

        var itemsArr = items.ToArray();
        var result = mathChannel.SendToPlatform("add", itemsArr);

        if (result is double dbl)
            labelResult.Text = dbl.ToString();
    }

	private object PlatformChannelView_OnReceiveFromPlatform(string messageId, object parameters)
	{
        return null;
	}
}