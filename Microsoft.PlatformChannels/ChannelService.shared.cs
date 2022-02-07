﻿using System;
using System.Collections.Generic;
using Microsoft.PlatformChannels.Platform;

namespace Microsoft.PlatformChannels
{
    public partial class ChannelService : IChannelService
    {
        public const string DEFAULT_INSTANCE_ID = "DEFAULT";

        public ChannelService(ChannelServiceConfiguration configuration = null)
        {
            Configuration = configuration ?? new ChannelServiceConfiguration();

            var provider = new ChannelProvider();

            PlatformProvider = provider;
            ManagedProvider = provider;

            Initialize();
        }

        public IPlatformChannelProvider PlatformProvider
        {
            get => PlatformChannelService.Provider;
            private set => PlatformChannelService.Provider = value;
        }

        public IManagedChannelProvider ManagedProvider { get; private set; }

        public ChannelServiceConfiguration Configuration { get; }

        public Channel Get(string channelId)
            => GetOrCreateChannel(channelId, null);

        public Channel GetOrCreateChannel(string channelId, string instanceId)
            => ManagedProvider.GetManagedInstance(channelId, instanceId ?? DEFAULT_INSTANCE_ID);
    }
}