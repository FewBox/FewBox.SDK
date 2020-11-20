using FewBox.Core.Utility.Net;
using FewBox.SDK.Config;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Core
{
    abstract class RestfulService
    {
        private ITryCatchService TryCatchService { get; set; }
        protected ICredentialService CredentialService { get; set; }
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected ILogger<RestfulService> Logger { get; set; }
        protected RestfulService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<RestfulService> logger)
        {
            this.TryCatchService = tryCatchService;
            this.CredentialService = credentialService;
            this.FewBoxSDKConfig = fewBoxSDKConfig;
            this.Logger = logger;
        }
        protected void PostInvoke(string url, string token, dynamic body)
        {
            Package<dynamic> package = new Package<dynamic> { Body = body };
            this.TryCatchService.Execute(() =>
            {
                RestfulUtility.Post<dynamic, dynamic>(url, token, package);
            });
            this.Logger.LogDebug("Sent {0} {1}", url, (object)body);
        }
    }
}