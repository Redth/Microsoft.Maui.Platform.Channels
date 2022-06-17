//
//  Init.swift
//  Sample
//
//  Created by Jon Dick on 2022-06-14.
//

import Foundation
import DotNetPlatformChannels

@objc
public class SampleChannels : NSObject {
    
    @objc
    public static func initChannels() {
        
        ChannelService.registerChannel("math", channelType: MathChannel.self)
        ChannelService.registerChannel("labelView", channelType: LabelViewChannel.self)
    }
}
