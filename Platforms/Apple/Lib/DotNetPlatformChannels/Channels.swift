import Foundation


@objc
public protocol ChannelMessageHandler : NSObjectProtocol {
    @objc
    func onChannelMessage(messageId:NSString, parameters:[NSObject]?) -> NSObject?
}



@objc
public protocol ChannelProvider : NSObjectProtocol {
    @objc
    func getDefaultInstanceId() -> NSString

    @objc
    func getInstance(channelId:NSString) -> Channel;

    @objc
    func getInstance(channelId:NSString, instanceId:NSString) -> Channel
}



@objc
public class ChannelService : NSObject
{
    //static let instance = ChannelService()

    static var channelClasses = [NSString : Channel.Type]()
    
    @objc
    public static var provider:ChannelProvider?

    @objc
    public static func register(channelId:NSString, channelClass:Channel.Type)
    {
        channelClasses[channelId] = channelClass;
    }
    
    @objc
    public static func getOrCreateChannel(channelId:NSString) throws -> Channel
    {
        let provider = provider!
        return provider.getInstance(channelId: channelId, instanceId: provider.getDefaultInstanceId())
    }

    @objc
    public static func getOrCreateChannel(channelId:NSString, instanceId:NSString) throws -> Channel
    {
        return provider!.getInstance(channelId: channelId, instanceId: instanceId)
    }
    
    @objc
    public static func create(channelId:NSString) throws -> Channel
    {
        if (channelClasses[channelId] != nil)
        {
            let ct = channelClasses[channelId]!
            
            return ct.init()
        }
        
        throw NSError.init()
    }
}

@objc
public protocol ChannelProtocol : NSObjectProtocol {
    @objc
    func handleMessageFromMaui(messageId: NSString, parameters: [NSObject]?) -> NSObject?
}


@objc
public class Channel : NSObject, ChannelProtocol
{
    @objc
    public required override init() {
    }
    
    @objc
    public var handler:ChannelMessageHandler?
    
    @objc
    public func handleMessageFromMaui(messageId: NSString, parameters: [NSObject]?) -> NSObject? {
        return nil;
    }
    
    public func sendMessageToMaui(messageId:NSString, parameters:[NSObject]?) -> NSObject?
    {
        return handler?.onChannelMessage(messageId: messageId, parameters: parameters)
    }
}
    

