import Foundation
import UIKit;
import os

// Protocols and classes here must line up with declarations in Microsoft.PlatformChannels.Binding.Apple/ApiDefinitions.cs
// to be exposed to C# and be marked @objc

@objc
public protocol ChannelMessageHandler {
    func sendMessageToDotNet (_ messageId: NSString, withArgs args: [NSObject]) -> NSObject?
}

@objc
public protocol ChannelProvider {
    func getDefaultInstanceId () -> NSString
    func getInstance (_ channelID: NSString, withInstance instanceID: NSString) -> Channel
    func disposeInstance (_ channelID: NSString, withInstance instanceID: NSString)
}

@objc
open class Channel : NSObject, ChannelMessageHandler {
    public required override init () { }
    
    public weak var managed: ChannelMessageHandler?;

    @objc
    public func setManagedHandler (_ handler: ChannelMessageHandler)
    {
        managed = handler;
    }
    
    @objc
    public func sendMessageToDotNet (_ messageId: NSString, withArgs args: [NSObject]) -> NSObject?
    {
        managed!.sendMessageToDotNet(messageId, withArgs: args)
    }
 
    @objc
    open func onChannelMessage (_ messageId: NSString, withArgs args: [NSObject]) -> NSObject?
    {
        return nil;
    }
}

@objc
open class ViewChannel : Channel {
    @objc
    open func getPlatformView () -> UIView?
    {
        return nil;
    }
}

@objc
public class ChannelService : NSObject {
    var services: [NSString:Channel.Type] = [:]

    @objc
    static public var channelProvider: ChannelProvider?;

    public static var shared = ChannelService()
    
    private override init () { }
    
    public func registerChannel (channelId: NSString, channelType: Channel.Type)
    {
        services[channelId] = channelType;
    }
    
    @objc
    public static func getOrCreate (_ channelId: NSString, instance instanceID: NSString) -> Channel?
    {
        return ChannelService.shared.getOrCreate (channelId, instance: instanceID)
    }

    public func getOrCreate (_ channelId: NSString, instance instanceID: NSString) -> Channel?
    {
        if services[channelId] != nil {
            return ChannelService.channelProvider?.getInstance(channelId, withInstance: instanceID)
        }
        else {
            return nil;
        }
    }
    
    @objc
    public static func create (_ channelId: NSString) -> Channel
    {
        return ChannelService.shared.create (channelId: channelId)
    }

    public func create (channelId: NSString) -> Channel
    {
        return services[channelId]!.init()
    }
}