using System;
using FewBox.SDK.Auth;
using FewBox.SDK.Core;

namespace FewBox.SDK.Web
{
    public class TestMQPlanHandler : IMQPlanHandler
    {
        public Func<PlanMessage, bool> Handle()
        {
            return (planMessage) =>
            {
                Console.WriteLine($"Handle {planMessage.Type.ToString()}");
                return true;
            };
        }
    }
}