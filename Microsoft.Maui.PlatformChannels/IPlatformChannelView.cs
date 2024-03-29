﻿using Microsoft.PlatformChannels;
using System;

namespace Microsoft.Maui.PlatformChannels;

public interface IPlatformChannelView : IView
{
	string ChannelTypeId { get; }

	string ChannelInstanceId { get; }

	object SendToPlatform(string id, object[] parameters);

	event ChannelMessageDelegate OnReceiveFromPlatform;
}
