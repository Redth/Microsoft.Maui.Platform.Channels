#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol ChannelMessageHandler <NSObject>

@required
-(nullable id) onChannelMessage: (NSString*)messageId withArgs:(nullable NSArray*)args;

@end

NS_ASSUME_NONNULL_END
