namespace PrintingMonitor.Printer.Notification
{
    interface INotificationDispatcherFactory<in T> where T : class
    {
        INotificationDispatcher<T> Get();
    }
}
