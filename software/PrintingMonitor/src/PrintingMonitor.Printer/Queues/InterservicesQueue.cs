using System.Threading.Channels;
using System.Threading.Tasks;

namespace PrintingMonitor.Printer.Queues
{
    public class InterservicesQueue<T> : IInterservicesQueue<T> where T : class
    {
        private readonly Channel<T> _channel;

        public InterservicesQueue()
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
