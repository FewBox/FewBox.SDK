using System;
using FewBox.SDK.Core;
using FewBox.SDK.Mail;

namespace FewBox.SDK.Web
{
    public class TestMQMailHandler : IMQMailHandler
    {
        public Func<EmailMessage, bool> Handle()
        {
            return (emailMessage) =>
            {
                Console.WriteLine($"Handle {emailMessage.Name}");
                return true;
            };
        }
    }
}