using FewBox.SDK.Auth;
using FewBox.SDK.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FewBox.Sdk.UnitTest
{
    [TestClass]
    public class PlanMQUnitTest
    {
        private IPlanService PlanService { get; set; }
        private string CustomerEmail { get; set; }
        private string ProductName { get; set; }

        [TestInitialize]
        public void Init()
        {
            var fewBoxSDKConfig = new FewBoxSDKConfig
            {
                MQ = new MQConfig
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "fewbox",
                    Password = "landpy",
                    Exchange = ""
                }
            };
            var logger = new Mock<ILogger<MQPlanService>>();
            this.PlanService = new MQPlanService(
                fewBoxSDKConfig,
                logger.Object);
            this.CustomerEmail = "test@fewbox.com";
            this.ProductName = "figma-library";
        }

        [TestMethod]
        public void TestStartFreePlan()
        {
            this.PlanService.StartFreePlan(this.CustomerEmail, this.ProductName);
        }

        [TestMethod]
        public void TestUpgradeProPlan()
        {
            this.PlanService.UpgradeProPlan(this.CustomerEmail, this.ProductName);
        }

        [TestMethod]
        public void TestQuitProPlan()
        {
            this.PlanService.QuitProPlan(this.CustomerEmail, this.ProductName);
        }
    }
}
