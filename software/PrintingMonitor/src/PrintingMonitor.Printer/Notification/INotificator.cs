using System;
using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Notification
{
    public interface INotificator<out T> where T: class
    {
        void Subscribed(object key, Func<T, Task> handler);

        void Unsubscribed(object key);
    }
}
