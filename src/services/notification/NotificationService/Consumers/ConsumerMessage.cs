using CommonNamespace;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consumers
{
    public class ConsumerMessage : IConsumer<MessageDto>
    {
        public Task Consume(ConsumeContext<MessageDto> context)
        {
            Console.WriteLine("I'm in");
            return Task.CompletedTask;
        }
    }
}
