#import <objc/runtime.h>
#import "ChannelService.h"
#import "ChannelProvider.h"
#import "Channel.h"

@implementation ChannelService

static ChannelService* channelServiceInstance = nil;

+(ChannelService*) getInstance
{
    if (channelServiceInstance == nil)
        channelServiceInstance = [ChannelService init];
    return channelServiceInstance;
}

+(void) setProvider: (id<ChannelProvider>)instance
{
    [[self getInstance] setChannelProvider:instance];
}

static NSMutableDictionary<NSString*, Class> *channelClasses;


+(void) registerChannel:(NSString*)channelId channelType:(Class)type
{
    if (channelClasses == nil)
        channelClasses = [NSMutableDictionary dictionary];
    
    [channelClasses setValue:type forKey:channelId];
}

+(Channel<ChannelMessageHandler>*) getOrCreate:(NSString*)channelId instance:(NSString*)instanceId
{
    if (channelClasses == nil)
        channelClasses = [NSMutableDictionary dictionary];
    
    Class channelClass = [channelClasses valueForKey:channelId];
    
    if (channelClass != nil)
    {
        return [[[self getInstance] channelProvider] getInstance:channelId withInstance:instanceId ];
    }
    
    return nil;
}

+(Channel<ChannelMessageHandler>*) create:(NSString*)channelId
{
    if (channelClasses == nil)
        channelClasses = [NSMutableDictionary dictionary];
    
    Class channelClass = [channelClasses valueForKey:channelId];
 
    return [channelClass init];
}


- (id<ChannelProvider>)channelProvider {
    return objc_getAssociatedObject(self, @selector(channelProvider));
}

-(void)setChannelProvider:(id<ChannelProvider>)provider {
    objc_setAssociatedObject(self, @selector(channelProvider), provider, OBJC_ASSOCIATION_RETAIN_NONATOMIC);
}


@end
