//using DotNetPlatformChannels;
using Foundation;
using ObjCRuntime;
//using ObjectiveC;

namespace Microsoft.PlatformChannels.Platform
{
	
	partial interface IChannelProtocol
	{
		
	}
	
	partial class Channel
	{
		public void SetManagedHandler(IChannelMessageHandler handler)
			=> Handler = handler;

		public IChannelMessageHandler? GetManagedHandler()
			=> Handler;
	}

	
	partial interface IChannelMessageHandler
	{
		
	}

	partial class ChannelProvider
	{
		
	}

	partial class ChannelService
	{
		public static Channel Create(string channelId)
		{
			var c = Create(channelId, out NSError? err);

			if (err is not null)
				throw new NSErrorException(err);

			return c!;
		}
	}
}
