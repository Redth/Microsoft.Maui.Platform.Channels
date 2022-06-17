#import <Foundation/Foundation.h>
#import <DotNetPlatformChannels/Channel.h>
#import <DotNetPlatformChannels/ChannelMessageHandler.h>

NS_ASSUME_NONNULL_BEGIN

@protocol ChannelProvider <NSObject>

@required
-(NSString*)getDefaultInstanceId;

-(Channel<ChannelMessageHandler>*) getInstance:(NSString*)channelId withInstance:(NSString*)instanceId;

-(void)disposeInstance:(NSString*)channelId withInstance:(NSString*)instanceId;

@end

NS_ASSUME_NONNULL_END
