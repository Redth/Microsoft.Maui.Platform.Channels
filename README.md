# Microsoft.Maui.Platform.Channels
A simple bridge for messaging between .NET and iOS/MacCatalyst/Android Platforms at runtime


## ViewChannels
1. Create your platform ViewChannel implementations using whatever platform specific code, libraries/sdks, etc. you like: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/Platforms/Android/sample/src/main/java/com/microsoft/dotnet/platformchannels/sample/LabelViewChannel.java#L19-L23
2. Reference your platform project from your .NET MAUI project: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/SamplePlatformChannels.csproj#L44
3. Use the `PlatformChannelView` MAUI control to display your ViewChannel: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml#L17
4. Send messages across to the platform: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml.cs#L18
5. You can receive messages originating from the platform too: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml.cs#L48
