using EasyNetQ.AutoSubscribe;
using Microliu.Core.EventBusRabbitMQ;
using System.Threading.Tasks;

namespace NoticeService.Models
{
    public class ClientMessageConsumer : IConsumeAsync<ClientMessage>
    {
        [AutoSubscriberConsumer(SubscriptionId = "ClientMessageService.ZapQuestion")]
        public Task ConsumeAsync(ClientMessage message)
        {
            // Your business logic code here
            // eg.Build one email to client via SMTP service
            // Sample console code
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine("Consume one message from RabbitMQ : {0}, I will send one email to client.", message.ClientName);
            System.Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}
