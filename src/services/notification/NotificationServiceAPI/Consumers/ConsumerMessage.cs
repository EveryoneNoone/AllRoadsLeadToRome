using AllRoadsLeadToRome.Core.MassTransit.Enums;
using AllRoadsLeadToRome.Core.MassTransit.Messages;
using Application;
using Infrustructure;
using MassTransit;

namespace NotificationServiceAPI.Consumers
{
    public class ConsumerMessage : IConsumer<MessageDto>
    {
        private readonly MongoDBService _dbService;

        public ConsumerMessage(MongoDBService dbService)
        {
            _dbService = dbService;
        }

        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            var receiver = context.Message;

            var template = await _dbService.GetTemplateAsync(receiver.TemplateName, receiver.TypeNotification);

            var message = PrepareWorker.PrepareMessage(template.Value, receiver.Content);
            ReceiverInfo receiverInfo = new ReceiverInfo
            {
                Message = message,
                NotificationType = receiver.TypeNotification,
                Receiver = receiver.Receiver
            };
            switch (context.Message.TypeNotification)
            {
                case NotificationType.None:
                    break;
                case NotificationType.Sms:
                    SmsWorker smsWorker = new SmsWorker(_dbService);
                    await smsWorker.SendAsync(receiverInfo);
                    break;
                case NotificationType.Email:
                    EmailWorker emailWorker = new EmailWorker(_dbService);
                    await emailWorker.SendAsync(receiverInfo);
                    break;
                case NotificationType.Push:
                    PushWorker pushWorker = new PushWorker(_dbService);
                    await pushWorker.SendAsync(receiverInfo);
                    break;
            }
        }
    }
}
