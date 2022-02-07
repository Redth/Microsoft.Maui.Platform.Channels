#xcodebuild archive -scheme DotNetPlatformChannels -configuration Release -destination 'generic/platform=iOS' -archivePath './build/DotNetPlatformChannels.framework-iphoneos.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES
#xcodebuild archive -scheme DotNetPlatformChannels -configuration Release -destination 'generic/platform=iOS Simulator' -archivePath './build/DotNetPlatformChannels.framework-iphonesimulator.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES
#xcodebuild archive -scheme DotNetPlatformChannels -configuration Release -destination 'platform=macOS,arch=x86_64,variant=Mac Catalyst' -archivePath './build/DotNetPlatformChannels.framework-catalyst.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES
#
#xcodebuild -create-xcframework -framework './build/DotNetPlatformChannels.framework-iphonesimulator.xcarchive/Products/Library/Frameworks/DotNetPlatformChannels.framework' \
#	-framework './build/DotNetPlatformChannels.framework-iphoneos.xcarchive/Products/Library/Frameworks/DotNetPlatformChannels.framework' \
#	-framework './build/DotNetPlatformChannels.framework-catalyst.xcarchive/Products/Library/Frameworks/DotNetPlatformChannels.framework' \
#	-output './build/DotNetPlatformChannels.xcframework'
#
#
#sharpie bind -n 'Microsoft.PlatformChannels.Platform' -o './build/' -framework './build/DotNetPlatformChannels.framework-iphoneos.xcarchive/Products/Library/Frameworks/DotNetPlatformChannels.framework' -sdk iphoneos15.2




xcodebuild archive -scheme Sample -configuration Release -destination 'generic/platform=iOS' -archivePath './build/Sample.framework-iphoneos.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES
xcodebuild archive -scheme Sample -configuration Release -destination 'generic/platform=iOS Simulator' -archivePath './build/Sample.framework-iphonesimulator.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES
xcodebuild archive -scheme Sample -configuration Release -destination 'platform=macOS,arch=x86_64,variant=Mac Catalyst' -archivePath './build/Sample.framework-catalyst.xcarchive' SKIP_INSTALL=NO BUILD_LIBRARIES_FOR_DISTRIBUTION=YES

xcodebuild -create-xcframework -framework './build/Sample.framework-iphonesimulator.xcarchive/Products/Library/Frameworks/Sample.framework' \
    -framework './build/Sample.framework-iphoneos.xcarchive/Products/Library/Frameworks/Sample.framework' \
    -framework './build/Sample.framework-catalyst.xcarchive/Products/Library/Frameworks/Sample.framework' \
    -output './build/Sample.xcframework'
