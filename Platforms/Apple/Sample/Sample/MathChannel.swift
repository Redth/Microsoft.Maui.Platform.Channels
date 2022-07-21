//
//  MathChannel.swift
//  Sample
//
//  Created by Jon Dick on 2022-06-14.
//

import Foundation
import DotNetPlatformChannels

class MathChannel : Channel {
    override func onChannelMessage (_ messageId: NSString, withArgs args: [NSObject]) -> NSObject? {
        let result = NSObject()

        if (messageId == "add") {
            if let a = args[0] as? NSNumber {
                if let b = args[1] as? NSNumber {
                    return NSNumber(value: a.doubleValue + b.doubleValue)
                }
            }
        }
        
        return result;
    }
}