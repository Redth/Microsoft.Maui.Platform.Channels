using System;
using System.Collections.Generic;

namespace Microsoft.Maui.Platform.Channels
{
    public partial class ChannelBroker
    {
        public ChannelBroker(ChannelBrokerConfiguration configuration = null)
        {
            Configuration = configuration ?? new ChannelBrokerConfiguration();
            Init();
        }

        public ChannelBrokerConfiguration Configuration { get; set; }

    }
}