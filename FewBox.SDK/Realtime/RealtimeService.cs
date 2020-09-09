using System;
using FewBox.SDK.Config;
using FewBox.SDK.Core;

namespace FewBox.SDK.Realtime
{
    class RealtimeService : RestfulService, IRealtimeService
    {
        public RealtimeService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig)
        : base(tryCatchService, credentialService, fewBoxSDKConfig)
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
