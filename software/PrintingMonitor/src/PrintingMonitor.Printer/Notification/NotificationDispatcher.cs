using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Notification
{
    internal class NotificationDispatcher<T> : INotificator<T>, INotificationDispatcher<T> where T : class
    {
        private readonly IDictionary<object, Func<T, Task>> _handlers =
            new ConcurrentDictionary<object, Func<T, Task>>();

        public void Subscribed(object key, Func<T, Task> handler)
        {
            _handlers.Add(key, handler);
        }

        public void Unsubscribed(object key)
        {
            _handlers.Remove(key);
        }

        public async Task Notification(T data)
        {
            foreach (var handler in _handlers)
            {
                await handler.Value(data);
            }
        }

        public bool IsHasSubscribers => _handlers.Count != 0;
    }
}
