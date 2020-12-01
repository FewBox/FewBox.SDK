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

        public void QuitProPlan(string email, string product)
        {
            this.Publish(QueueNames.Plan,new PlanMessage { Type = PlanType.Free });
        }

        public void StartFreePlan(string email, string product)
        {
            this.Publish(QueueNames.Plan, new PlanMessage { Type = PlanType.Free });
        }

        public void UpgradeProPlan(string email, string product)
        {
            this.Publish(QueueNames.Plan, new PlanMessage { Type = PlanType.Pro });
        }
    }
}
