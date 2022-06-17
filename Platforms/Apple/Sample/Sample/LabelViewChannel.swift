//
//  LabelChannel.swift
//  Sample
//
//  Created by Jon Dick on 2022-06-14.
//


import Foundation
import DotNetPlatformChannels
import UIKit

class LabelViewChannel : ViewChannel, ChannelMessageHandler, ChannelViewProvider {
    
    func onChannelMessage(_ messageId: String, withArgs args: [Any]?) -> Any? {
        
        if (messageId == "setText") {
            if let a = args![0] as? NSString {
                label?.text = String(a)
            }
        }
        return nil;
    }
    
    var label: UILabel? = nil;
    
    func getPlatformView() -> UIView {
        if (label == nil) {
            label = UILabel(frame: CGRect())
        }
        return label!;
    }
}
