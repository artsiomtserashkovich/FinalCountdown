using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Notification
{
    public interface INotificationDispatcher<in T> where T : class
    {
        Task Notification(T data);

        bool HasSubscribers { get; }
    }
}
