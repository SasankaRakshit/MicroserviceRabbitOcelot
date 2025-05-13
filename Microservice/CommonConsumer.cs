using MassTransit;
using Microservice.Model;

namespace Microservice
{
    public class CommonConsumer:IConsumer<Common>
    {
        public async Task Consume(ConsumeContext<Common> context)
        {
            // Handle the message here
            Console.WriteLine($"Received message: {context.Message.Name}");
            await Task.CompletedTask;
        }
    }
   
}
