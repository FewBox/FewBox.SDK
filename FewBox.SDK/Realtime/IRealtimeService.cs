namespace FewBox.SDK.Realtime
{
    public interface IRealtimeService
    {
        void NotifyAll(string message, string description);
    }
}