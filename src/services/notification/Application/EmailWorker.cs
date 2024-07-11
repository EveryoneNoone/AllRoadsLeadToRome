using Infrustructure;

namespace Application
{
    public class EmailWorker : IWorker
    {
        private readonly MongoDBService dBService;

        public EmailWorker(MongoDBService mongoDBService)
        {
            dBService = mongoDBService;
        }

        public async Task SendAsync(ReceiverInfo receiverInfo)
        {
            //Here send email
            receiverInfo.SendResult = true;
            receiverInfo.SendDateTime = DateTime.Now;
            await dBService.CreateNotificationAsync(receiverInfo);
        }
    }
}
