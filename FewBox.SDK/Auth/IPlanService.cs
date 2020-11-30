namespace FewBox.SDK.Auth
{
    public interface IPlanService
    {
        void StartFreePlan(string email, string product);
        void UpgradeProPlan(string email, string product);
        void QuitProPlan(string email, string product);
    }
}