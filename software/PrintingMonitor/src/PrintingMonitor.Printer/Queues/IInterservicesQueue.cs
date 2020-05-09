using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Queues
{
    public interface IInterservicesQueue<T> where T : class
    {
        Task AddMessage(T message);

        Task<T> GetMessage();
    }
}
