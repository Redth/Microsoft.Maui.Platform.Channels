# Microsoft.Maui.Platform.Channels
A simple bridge for messaging between .NET and iOS/MacCatalyst/Android Platforms at runtime


## Getting Started

 1. Install prerequisites:

    * Install [Xcode](https://developer.apple.com/xcode/).
    * Install [Android Studio](https://developer.android.com/studio) and JDK 17.
    * Install the latest [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and `maui` workload.

 2. Build `Platforms`:
    ```shell
        dotnet build Platforms/build.proj
    ```

 3. Build `Microsoft.PlatformChannels.sln`:
    ```shell
        dotnet build Microsoft.PlatformChannels.sln
    ```

 4. Run the sample
    ```shell
        dotnet build SamplePlatformChannels/SamplePlatformChannels.csproj -t:Run -f net8.0-android
        dotnet build SamplePlatformChannels/SamplePlatformChannels.csproj -t:Run -f net8.0-ios
        dotnet build SamplePlatformChannels/SamplePlatformChannels.csproj -t:Run -f net8.0-maccatalyst
    ```


## ViewChannels
1. Create your platform ViewChannel implementations using whatever platform specific code, libraries/sdks, etc. you like: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/Platforms/Android/sample/src/main/java/com/microsoft/dotnet/platformchannels/sample/LabelViewChannel.java#L19-L23
2. Reference your platform project from your .NET MAUI project: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/SamplePlatformChannels.csproj#L44
3. Use the `PlatformChannelView` MAUI control to display your ViewChannel: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml#L17
4. Send messages across to the platform: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml.cs#L18
5. You can receive messages originating from the platform too: https://github.com/Redth/Microsoft.Maui.Platform.Channels/blob/main/SamplePlatformChannels/MainPage.xaml.cs#L48
