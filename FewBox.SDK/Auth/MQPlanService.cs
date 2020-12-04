using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Auth
{
    class MQPlanService : MQService<PlanMessage>, IPlanService
    {
        public MQPlanService(FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQPlanService> logger)
        : base(fewBoxSDKConfig, logger)
        {
        }

        public void QuitProPlan(PlanCustomer customer, PlanProduct product)
        {
            this.Publish(QueueNames.Plan, new PlanMessage
            {
                Type = PlanType.Free,
                Customer = customer,
                Product = product
            });
        }

        public void StartFreePlan(PlanCustomer customer, PlanProduct product)
        {
            this.Publish(QueueNames.Plan, new PlanMessage
            {
                Type = PlanType.Free,
                Customer = customer,
                Product = product
            });
        }

        public void UpgradeProPlan(PlanCustomer customer, PlanProduct product)
        {
            this.Publish(QueueNames.Plan, new PlanMessage
            {
                Type = PlanType.Pro,
                Customer = customer,
                Product = product
            });
        }
    }
}
