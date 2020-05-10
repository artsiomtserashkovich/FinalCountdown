using System;
using System.Threading.Tasks;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal interface ICommunicationConnection
    {
        void SendCommand(string command);

        void SubscribedToResponse(Func<string, Task> handler, object key);
    }
}
