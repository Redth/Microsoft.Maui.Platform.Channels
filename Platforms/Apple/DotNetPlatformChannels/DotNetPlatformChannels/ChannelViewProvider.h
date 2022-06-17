//
//  ChannelViewProvider.h
//  DotNetPlatformChannels
//
//  Created by Jon Dick on 2022-06-14.
//

#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>

@protocol ChannelViewProvider <NSObject>

-(UIView*) getPlatformView;

@end
