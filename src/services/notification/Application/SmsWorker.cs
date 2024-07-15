using Infrustructure;

namespace Application
{
    public class SmsWorker : IWorker
    {
        private readonly MongoDBService dBService;
        public SmsWorker(MongoDBService mongodBService)
        {
            dBService = mongodBService;
        }

        public async Task SendAsync(ReceiverInfo receiverInfo)
        {
            //Here sending sms
            receiverInfo.SendResult = true;
            receiverInfo.SendDateTime = DateTime.Now;
            await dBService.CreateNotificationAsync(receiverInfo);
        }
    }
}
