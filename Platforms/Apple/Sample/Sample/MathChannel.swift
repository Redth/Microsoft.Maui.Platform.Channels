//
//  MathChannel.swift
//  Sample
//
//  Created by Jon Dick on 2022-06-14.
//

import Foundation
import DotNetPlatformChannels

class MathChannel : Channel, ChannelMessageHandler {
    
    func onChannelMessage(_ messageId: String, withArgs args: [Any]?) -> Any? {
        let result = NSObject()

        if (messageId == "add") {
            
            if let a = args![0] as? NSNumber {
                if let b = args![1] as? NSNumber {
                    return a.decimalValue + b.decimalValue
                }
            }
        }
        
        return result;
    }
}
