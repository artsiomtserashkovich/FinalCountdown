using System.Threading.Channels;
using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Queues
{
    public class Queue<T> : IQueue<T> where T : class
    {
        private readonly Channel<T> _channel;

        public Queue()
        {
            _channel = Channel.CreateUnbounded<T>();
        }

        public async Task AddMessage(T message)
        {
            await _channel.Writer.WriteAsync(message);
        }

        public Task<T> GetMessage()
        {
            return _channel.Reader.ReadAsync().AsTask();
        }
    }
}
