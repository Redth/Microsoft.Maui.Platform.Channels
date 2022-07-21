CURRENT:=$(shell dirname $(realpath $(firstword $(MAKEFILE_LIST))))

all::
	rm -fr build/
	xcodebuild -configuration Debug -sdk iphonesimulator15.5
	xcodebuild -configuration Debug -sdk iphoneos15.5
	xcodebuild -configuration Debug -destination 'platform=macOS,variant=Mac Catalyst' -scheme DotNetPlatformChannels SYMROOT=$(CURRENT)/build
	xcodebuild -create-xcframework -framework build/Debug-iphoneos/DotNetPlatformChannels.framework  -framework build/Debug-iphonesimulator/DotNetPlatformChannels.framework -framework build/Debug-maccatalyst/DotNetPlatformChannels.framework -output build/DotNetPlatformChannels.xcframework

clean:
	rm -fr build/