using System.Threading.Tasks;
using PrintingMonitor.Printer.Models;

namespace PrintingMonitor.Printer.Queries
{
    public interface ITemperaturesQuery
    {
        Task<Temperatures> Get();
    }
}
