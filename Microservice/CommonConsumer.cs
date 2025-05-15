using MassTransit;
using Test;

namespace Microservice
{
    public class CommonConsumer:IConsumer<CommonTest>
    {
        public async Task Consume(ConsumeContext<CommonTest> context)
        {
            // Handle the message here
            Console.WriteLine($"Received message: {context.Message.Name}");
            await Task.CompletedTask;
        }
    }
   
}
