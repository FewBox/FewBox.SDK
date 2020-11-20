using System;
using System.Collections.Generic;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Mail
{
    class MQMailService : MQService, IMailService
    {
        public MQMailService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQMailService> logger)
        : base(tryCatchService, credentialService, fewBoxSDKConfig, logger)
        {
        }

        public void OpsNotification(string name, string content, IList<string> toAddresses)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.PostInvoke(QueueNames.Mail, new EmailMessage { Name = name, Content = content, ToAddresses = toAddresses });
        }
    }
}
