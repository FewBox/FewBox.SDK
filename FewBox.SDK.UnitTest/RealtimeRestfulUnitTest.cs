using System;
using FewBox.SDK.Config;
using FewBox.SDK.Core;
using FewBox.SDK.Realtime;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FewBox.Sdk.UnitTest
{
    //[TestClass]
    public class RealtimeRestfulUnitTest
    {
        private IRealtimeService RealtimeService { get; set; }
        [TestInitialize]
        public void Init()
        {
            var tryCatchServiceMock = new Mock<ITryCatchService>();
            tryCatchServiceMock.Setup(t=>t.Execute(It.IsAny<Action>()))
            .Callback(delegate(){});
            var credentialServiceMock = new Mock<ICredentialService>();
            var fewBoxSDKConfig = new Mock<FewBoxSDKConfig>();
            var logger = new Mock<ILogger<RestfulRealtimeService>>();
            this.RealtimeService = new RestfulRealtimeService(
                tryCatchServiceMock.Object,
                credentialServiceMock.Object,
                fewBoxSDKConfig.Object,
                logger.Object);
        }
          
        [TestMethod]
        public void TestMethod1()
        {
            this.RealtimeService.NotifyAll("FewBox", "Landpy");
        }
    }
}
