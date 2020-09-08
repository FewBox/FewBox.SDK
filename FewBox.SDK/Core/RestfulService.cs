using FewBox.Core.Utility.Net;
using FewBox.SDK.Config;

namespace FewBox.SDK.Core
{
    abstract class RestfulService
    {
        private ITryCatchService TryCatchService { get; set; }
        protected ICredentialService CredentialService { get; set; }
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected RestfulService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig)
        {
            this.TryCatchService = tryCatchService;
            this.CredentialService = credentialService;
            this.FewBoxSDKConfig = fewBoxSDKConfig;
        }
        protected void PostInvoke(string url, string token, dynamic body)
        {
            Package<dynamic> package = new Package<dynamic> { Body = body };
            this.TryCatchService.Execute(() =>
            {
                RestfulUtility.Post<dynamic, dynamic>(url, token, package);
            }, this.IsNeedNotification());
        }

        protected bool IsNeedNotification()
        {
            return false;
        }
    }
}