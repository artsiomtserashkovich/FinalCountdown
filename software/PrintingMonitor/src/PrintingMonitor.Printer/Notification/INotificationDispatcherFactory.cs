namespace PrintingMonitor.Printer.Notification
{
    public interface INotificationDispatcherFactory<in T> where T : class
    {
        INotificationDispatcher<T> Create();
    }
}
