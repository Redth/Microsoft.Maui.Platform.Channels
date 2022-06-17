#import <Foundation/Foundation.h>
#import <DotNetPlatformChannels/ChannelMessageHandler.h>

NS_ASSUME_NONNULL_BEGIN

@interface Channel : NSObject

@property (weak) id <ChannelMessageHandler> managedHandler;

-(id) sendMessageToDotNet: (NSString*)messageId withArgs:(NSArray*)args;

@end

NS_ASSUME_NONNULL_END
