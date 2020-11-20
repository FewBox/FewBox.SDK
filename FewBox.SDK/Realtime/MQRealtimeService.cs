using System;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Realtime
{
    class MQRestfulRealtimeService : MQService, IRealtimeService
    {
        public MQRestfulRealtimeService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQRestfulRealtimeService> logger)
        : base(tryCatchService, credentialService, fewBoxSDKConfig, logger)
        {
        }

        public void NotifyAll(string message, string desciption)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.PostInvoke(QueueNames.Realtime, new {}); // Todo
        }
    }
}