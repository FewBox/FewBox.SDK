using FewBox.SDK.Core;

namespace FewBox.SDK.Auth
{
    public interface IPlanService
    {
        void StartFreePlan(PlanCustomer customer, PlanProduct product);
        void UpgradeProPlan(PlanCustomer customer, PlanProduct product);
        void QuitProPlan(PlanCustomer customer, PlanProduct product);
    }
}