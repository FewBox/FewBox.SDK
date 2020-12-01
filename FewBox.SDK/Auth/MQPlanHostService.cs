using System;
using FewBox.SDK.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Auth
{
    public class MQPlanHostService : MQHostService<PlanMessage>
    {
        public MQPlanHostService(IServiceScopeFactory serviceScopeFactory, IMQListenerService<PlanMessage> mqListenerService, ILogger<MQHostService<PlanMessage>> logger)
        : base(serviceScopeFactory, mqListenerService, logger)
        {
        }

        protected override Func<PlanMessage, bool> GetFunc()
        {
            using (var scope = this.ServiceScopeFactory.CreateScope())
            {
                IMQPlanHandler scopedMQPlanHandler = scope.ServiceProvider.GetRequiredService<IMQPlanHandler>();
                return scopedMQPlanHandler.Handle();
            }
        }

        protected override string GetQueue()
        {
            return QueueNames.Plan;
        }
    }
}