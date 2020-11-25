using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FewBox.SDK.Config;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FewBox.Sdk.UnitTest
{
    [TestClass]
    public class RealtimeMQUnitTest
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
                    Port = 31849,
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

        [TestMethod]
        public void TestReseiveOpsNotification()
        {
            this.MailService.ReceiveOpsNotification((emailMessage) =>
            {
                StringBuilder toAddresses = new StringBuilder();
                foreach (var toAddress in emailMessage.ToAddresses)
                {
                    toAddresses.AppendFormat("{0};", toAddress);
                }
                Debug.WriteLine($"{emailMessage.Name}-{emailMessage.Content}:{toAddresses.ToString()}");
            });
        }
    }
}
