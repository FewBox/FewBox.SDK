using System;

namespace FewBox.SDK.Core
{
    public interface IMQListenerService<T> : IDisposable
    {
        void Start(string queue, Func<T, bool> func);
        void Stop();
    }
}