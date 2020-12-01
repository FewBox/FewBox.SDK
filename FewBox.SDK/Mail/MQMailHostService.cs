using System;
using FewBox.SDK.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Mail
{
    public class MQMailHostService : MQHostService<EmailMessage>
    {
        private IMQMailHandler MQMailHandler { get; set; }
        public MQMailHostService(IServiceScopeFactory serviceScopeFactory, IMQListenerService<EmailMessage> mqListenerService, ILogger<MQHostService<EmailMessage>> logger)
        : base(serviceScopeFactory, mqListenerService, logger)
        {
        }

        protected override Func<EmailMessage, bool> GetFunc()
        {
            using (var scope = this.ServiceScopeFactory.CreateScope())
            {
                IMQMailHandler scopedMQMailHandler = scope.ServiceProvider.GetRequiredService<IMQMailHandler>();
                return scopedMQMailHandler.Handle();
            }
        }

        protected override string GetQueue()
        {
            return QueueNames.Mail;
        }
    }
}