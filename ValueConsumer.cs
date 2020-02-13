using System;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitActiveMQIssue
{
    public class ValueConsumer: IConsumer<Value>
    {
         public async Task Consume(ConsumeContext<Value> context) =>  await Console.Out.WriteLineAsync($"received: {context.Message.message}");
    }
}
