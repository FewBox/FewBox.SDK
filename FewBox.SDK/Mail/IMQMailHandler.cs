using System;
using FewBox.SDK.Core;

namespace FewBox.SDK.Mail
{
    public interface IMQMailHandler
    {
        Func<EmailMessage, bool> Handle();
    }
}