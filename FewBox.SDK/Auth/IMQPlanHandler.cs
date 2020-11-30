using System;
using FewBox.SDK.Core;

namespace FewBox.SDK.Auth
{
    public interface IMQPlanHandler
    {
        Func<PlanMessage, bool> Handle();
    }
}