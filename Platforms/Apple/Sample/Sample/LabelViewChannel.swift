//
//  LabelChannel.swift
//  Sample
//
//  Created by Jon Dick on 2022-06-14.
//


import Foundation
import DotNetPlatformChannels
import UIKit

class LabelViewChannel : ViewChannel {
    override func onChannelMessage (_ messageId: NSString, withArgs args: [NSObject]) -> NSObject? {
        if (messageId == "setText") {
            if let a = args[0] as? NSString {
                label?.text = String(a)
            }
        }
        return nil;
    }
    
    var label: UILabel? = nil;
    
    override func getPlatformView () -> UIView {
        if (label == nil) {
            label = UILabel(frame: CGRect(x: 0, y: 0, width: 400, height: 100))
            label!.textColor = UIColor.black
            label!.textAlignment = NSTextAlignment.left
        }

        return label!;
    }
}
