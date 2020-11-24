using System;
using System.Collections.Generic;
using FewBox.Core.Utility.Net;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Mail
{
    class RestfulMailService : RestfulService, IMailService
    {
        public RestfulMailService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<RestfulMailService> logger)
        : base(tryCatchService, credentialService, fewBoxSDKConfig, logger)
        {
        }

        public void ReceiveOpsNotification(Action<EmailMessage> action)
        {
            throw new NotImplementedException();
        }

        public void SendOpsNotification(string name, string content, IList<string> toAddresses)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.PostInvoke($"{this.FewBoxSDKConfig.MailEndPoint.UrlPrefix}/opsnotification", token, new { Name = name, Content = content, ToAddresses = toAddresses });
        }
    }
}
