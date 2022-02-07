//
//  Sample.swift
//  Sample
//
//  Created by Jon Dick on 2022-02-07.
//

import Foundation
 

@objc
public class Sample : NSObject
{
    @objc
    public static func setup()
    {
        ChannelService.register(channelId: "math", channelClass: MathChannel.Type)
    }
}

@objc
public class MathChannel : Channel
{
    public override func handleMessageFromMaui(messageId: NSString, parameters: [NSObject]?) -> NSObject? {
        
        if (messageId == "add") {
            
            
        }
        
        return nil;
    }
}
