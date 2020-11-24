using System;
using System.Collections.Generic;
using FewBox.SDK.Core;

namespace FewBox.SDK.Mail
{
    public interface IMailService
    {
        void SendOpsNotification(string name, string content, IList<string> toAddresses);
        void ReceiveOpsNotification(Action<EmailMessage> action);
    }
}