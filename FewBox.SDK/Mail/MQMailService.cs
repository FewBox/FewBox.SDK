using System;
using System.Collections.Generic;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Mail
{
    class MQMailService : MQService<EmailMessage>, IMailService
    {
        public MQMailService(FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQMailService> logger)
        : base(fewBoxSDKConfig, logger)
        {
            logger.LogDebug($"[MQ Init] {fewBoxSDKConfig.MQ.HostName}:{fewBoxSDKConfig.MQ.Port}");
        }

        public void SendOpsNotification(string name, string content, IList<string> toAddresses)
        {
            string token = String.Empty;
            // Todo: Need to validate by AK & SK.
            this.Publish(QueueNames.Mail, new EmailMessage { Name = name, Content = content, ToAddresses = toAddresses });
        }
    }
}
