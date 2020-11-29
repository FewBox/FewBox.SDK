using System.Collections.Generic;

namespace FewBox.SDK.Mail
{
    public interface IMailService
    {
        void SendOpsNotification(string name, string content, IList<string> toAddresses);
    }
}