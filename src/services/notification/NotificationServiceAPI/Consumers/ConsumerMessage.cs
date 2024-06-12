using MassTransit;
using AllRoadsLeadToRome.Core.MassTransit.Messages;
using AllRoadsLeadToRome.Core.MassTransit.Enums;
using Application;

namespace NotificationServiceAPI.Consumers
{
    public class ConsumerMessage : IConsumer<MessageDto>
    {
        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            switch (context.Message.TypeNotification) 
            {
                case NotificationType.None:
                    break;
                case NotificationType.Sms:
                    SmsWorker smsWorker = new SmsWorker();
                    break;
                case NotificationType.Email:
                    EmailWorker emailWorker = new EmailWorker();
                    break;
                case NotificationType.Push:
                    PushWorker pushWorker = new PushWorker();
                    break;
            }
        }
    }
}
