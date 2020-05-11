using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Connection
{
    public interface IOnConnectionAction
    {
        Task Execute();
    }
}
