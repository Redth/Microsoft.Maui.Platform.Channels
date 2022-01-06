namespace SamplePlatformChannels;
public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
        
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        

        var active = (bool)pcv.SendToPlatform("start");

        if (active)
            pcv.SendToPlatform("stop");
        else
            pcv.SendToPlatform("start");
    }

}