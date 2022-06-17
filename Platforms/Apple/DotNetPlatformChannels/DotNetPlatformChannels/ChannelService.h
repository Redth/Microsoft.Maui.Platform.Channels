#import <Foundation/Foundation.h>
#import <DotNetPlatformChannels/ChannelProvider.h>
#import <DotNetPlatformChannels/Channel.h>

NS_ASSUME_NONNULL_BEGIN

@interface ChannelService : NSObject

+(ChannelService*) getInstance;

+(void) registerChannel:(NSString*)channelId channelType:(Class)type;

+(Channel<ChannelMessageHandler>*) getOrCreate:(NSString*)channelId instance:(NSString*)instanceId;

+(Channel<ChannelMessageHandler>*) create:(NSString*)channelId;

@property (nonatomic, weak) id <ChannelProvider> channelProvider;


@end

NS_ASSUME_NONNULL_END
