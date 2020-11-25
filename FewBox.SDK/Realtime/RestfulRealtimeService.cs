﻿using System;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Realtime
{
    class RestfulRealtimeService : RestfulService, IRealtimeService
    {
        public RestfulRealtimeService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<RestfulRealtimeService> logger)
        : base(tryCatchService, credentialService, fewBoxSDKConfig, logger)
        {
        }

        public void NotifyAll(string message, string desciption)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.PostInvoke($"{this.FewBoxSDKConfig.RealtimeEndPoint.UrlPrefix}/hub", token, new { Message = message, Desciption = desciption });
        }
    }
}