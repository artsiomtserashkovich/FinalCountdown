using System.Threading.Tasks;
using PrintingMonitor.Printer.Models;

namespace PrintingMonitor.Printer.Queries
{
    public interface IPositionsQuery
    {
        Task<Positions> Get();
    }
}
