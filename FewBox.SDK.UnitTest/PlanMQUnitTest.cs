using FewBox.SDK.Auth;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FewBox.Sdk.UnitTest
{
    [TestClass]
    public class PlanMQUnitTest
    {
        private IPlanService PlanService { get; set; }
        private PlanCustomer Customer { get; set; }
        private PlanProduct Product { get; set; }

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
            this.Customer = new PlanCustomer
            {
                Email = "test@fewbox.com",
                Telephone = "13111111111",
                WeChat = "landpy"
            };
            this.Product = new PlanProduct
            {
                Name = "Smart",
                Count = 1
            };
        }

        [TestMethod]
        public void TestStartFreePlan()
        {
            this.PlanService.StartFreePlan(this.Customer, this.Product);
        }

        [TestMethod]
        public void TestUpgradeProPlan()
        {
            this.PlanService.UpgradeProPlan(this.Customer, this.Product);
        }

        [TestMethod]
        public void TestQuitProPlan()
        {
            this.PlanService.QuitProPlan(this.Customer, this.Product);
        }
    }
}
