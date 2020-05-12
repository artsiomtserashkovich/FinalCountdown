using System;
using System.Threading.Tasks;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal interface ICommunicationConnection
    {
        void SendCommand(string command);

        string ReadUntil(string key);

        string ReadLine();

        bool HasResponseToRead { get; }
    }
}
