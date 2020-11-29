using FewBox.Core.Utility.Formatter;
using FewBox.Core.Utility.Net;
using FewBox.SDK.Config;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Core
{
    abstract class RestfulService<T> where T : class
    {
        private ITryCatchService TryCatchService { get; set; }
        protected ICredentialService CredentialService { get; set; }
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected ILogger<RestfulService<T>> Logger { get; set; }
        protected RestfulService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<RestfulService<T>> logger)
        {
            this.TryCatchService = tryCatchService;
            this.CredentialService = credentialService;
            this.FewBoxSDKConfig = fewBoxSDKConfig;
            this.Logger = logger;
        }
        protected void PostInvoke(string url, string token, T body)
        {
            this.Logger.LogDebug("Sent {0} {1}", url, JsonUtility.Serialize<T>(body));
            Package<T> package = new Package<T> { Body = body };
            this.TryCatchService.Execute(() =>
            {
                object result = RestfulUtility.Post<T, object>(url, token, package);
                this.Logger.LogDebug("Receive {0}", JsonUtility.Serialize<object>(result));
            });
        }
    }
}