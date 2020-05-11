using System;
using System.Threading.Tasks;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal interface ICommunicationConnection
    {
        void SendCommand(string command);

        string ReadUntil(string key);

        bool HasResponseToRead { get; }
    }
}
