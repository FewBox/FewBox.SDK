using System;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Auth
{
    public class MQPlanHostService : MQHostService<PlanMessage>
    {
        private IMQPlanHandler MQPlanHandler { get; set; }
        public MQPlanHostService(IMQPlanHandler mqPlanHandler, IMQListenerService<PlanMessage> mqListenerService, ILogger<MQHostService<PlanMessage>> logger) : base(mqListenerService, logger)
        {
            this.MQPlanHandler = mqPlanHandler;
        }

        protected override Func<PlanMessage, bool> GetFunc()
        {
            return this.MQPlanHandler.Handle();
        }

        protected override string GetQueue()
        {
            return QueueNames.Plan;
        }
    }
}