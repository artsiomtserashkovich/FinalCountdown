using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Queues
{
    interface IQueue<T> where T : class
    {
        Task AddMessage(T message);

        Task<T> GetMessage();
    }
}
