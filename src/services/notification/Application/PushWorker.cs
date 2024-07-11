using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class PushWorker : IWorker
    {
        private readonly MongoDBService dBService;

        public PushWorker(MongoDBService mongoDBService)
        {
            dBService = mongoDBService;
        }

        public async Task SendAsync(ReceiverInfo receiverInfo)
        {
            //Here send push
            receiverInfo.SendResult = true;
            receiverInfo.SendDateTime = DateTime.Now;
            await dBService.CreateNotificationAsync(receiverInfo);
        }
    }
}
