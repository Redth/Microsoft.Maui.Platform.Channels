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
        ChannelService.shared.registerChannel(channelId: "math", channelType: MathChannel.self)
        ChannelService.shared.registerChannel(channelId: "labelView", channelType: LabelViewChannel.self)
    }
}