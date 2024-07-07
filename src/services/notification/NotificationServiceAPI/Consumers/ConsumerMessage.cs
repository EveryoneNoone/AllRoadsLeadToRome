using MassTransit;
using AllRoadsLeadToRome.Core.MassTransit.Messages;
using AllRoadsLeadToRome.Core.MassTransit.Enums;
using Application;
using NotificationServiceAPI.Services;
using Microsoft.Extensions.Options;
using Infrustructure;

namespace NotificationServiceAPI.Consumers
{
    public class ConsumerMessage : IConsumer<MessageDto>
    {
        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            var receiver = context.Message;

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            IOptions<Models.NotificationDatabaseSettings> options = Options.Create((Models.NotificationDatabaseSettings)configuration.GetSection("NotificationStoreDatabase"));
            var dbService = new MongoDBService(options);
            var template = await dbService.GetTemplateAsync(receiver.TemplateName, receiver.TypeNotification);

            var message = PrepareWorker.PrepareMessage(template.Value, receiver.Content);
            IReceiverInfo receiverInfo = new ReceiverInfo
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
                    SmsWorker smsWorker = new SmsWorker();
                    smsWorker.Send(receiverInfo);
                    break;
                case NotificationType.Email:
                    EmailWorker emailWorker = new EmailWorker();
                    emailWorker.Send(receiverInfo);
                    break;
                case NotificationType.Push:
                    PushWorker pushWorker = new PushWorker();
                    pushWorker.Send(receiverInfo);
                    break;
            }
        }
    }
}
