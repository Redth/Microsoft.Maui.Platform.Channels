CURRENT:=$(shell dirname $(realpath $(firstword $(MAKEFILE_LIST))))

build::
	xcodebuild -configuration Debug -sdk iphonesimulator15.5
	xcodebuild -configuration Debug -destination 'platform=macOS,variant=Mac Catalyst' -scheme Sample SYMROOT=$(CURRENT)/build

clean:
	rm -r build/