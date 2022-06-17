#import <objc/runtime.h>
#import "Channel.h"

@implementation Channel
- (id) sendMessageToDotNet: (NSString*)messageId withArgs:(NSArray*)args {
    return [[self managedHandler] onChannelMessage:messageId withArgs:args];
}

- (id<ChannelMessageHandler>)managedHandler {
    return objc_getAssociatedObject(self, @selector(managedHandler));
}

- (void)setManagedHandler:(id<ChannelMessageHandler>)handler {
    objc_setAssociatedObject(self, @selector(managedHandler), handler, OBJC_ASSOCIATION_RETAIN_NONATOMIC);
}

@end
