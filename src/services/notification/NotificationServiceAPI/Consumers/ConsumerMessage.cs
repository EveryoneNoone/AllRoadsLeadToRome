
using MassTransit;
using NotificationServiceAPI.Dto;

namespace NotificationServiceAPI.Consumers
{
    public class ConsumerMessage : IConsumer<MessageDto>
    {
        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            Console.WriteLine("I'm in");
        }
    }
}
