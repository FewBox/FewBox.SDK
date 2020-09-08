using System;

namespace FewBox.SDK.Core
{
    interface ITryCatchService
    {
        void Execute(Action action, bool isNeedNotification = false);
    }
}