using System;
using System.Collections.Generic;
using FewBox.Core.Utility.Net;
using FewBox.SDK.Config;
using FewBox.SDK.Core;

namespace FewBox.SDK.Mail
{
    class MailService : RestfulService, IMailService
    {
        public MailService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig)
        : base(tryCatchService, credentialService, fewBoxSDKConfig)
        {
        }

        public void OpsNotification(string name, string content, IList<string> toAddresses)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.PostInvoke($"{this.FewBoxSDKConfig.MailEndPoint.UrlPrefix}/opsnotification", token, new { Name = name, Content = content, ToAddresses = toAddresses });
        }
    }
}
