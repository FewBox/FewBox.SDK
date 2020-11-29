using System;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Mail
{
    public class MQMailHostService : MQHostService<EmailMessage>
    {
        private IMQMailHandler MQMailHandler { get; set; }
        public MQMailHostService(IMQMailHandler mqMailHandler, IMQListenerService<EmailMessage> mqListenerService, ILogger<MQHostService<EmailMessage>> logger) : base(mqListenerService, logger)
        {
            this.MQMailHandler = mqMailHandler;
        }

        protected override Func<EmailMessage, bool> GetFunc()
        {
            return this.MQMailHandler.Handle();
        }

        protected override string GetQueue()
        {
            return QueueNames.Mail;
        }
    }
}