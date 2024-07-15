using Infrustructure;

namespace Application
{
    public interface IWorker
    {
        public Task SendAsync(ReceiverInfo receiverInfo);
    }
}
