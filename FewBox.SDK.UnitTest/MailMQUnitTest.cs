using System.Collections.Generic;
using FewBox.SDK.Config;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FewBox.Sdk.UnitTest
{
    [TestClass]
    public class MailMQUnitTest
    {
        private IMailService MailService { get; set; }

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
            var logger = new Mock<ILogger<MQMailService>>();
            this.MailService = new MQMailService(
                fewBoxSDKConfig,
                logger.Object);
        }

        [TestMethod]
        public void TestSendOpsNotification()
        {
            this.MailService.SendOpsNotification("FewBox", "Wellcome to use our product.",
            new List<string> { "support@fewbox.com" });
        }
    }
}
