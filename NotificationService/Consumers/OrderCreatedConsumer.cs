using Contracts;
using Contracts.Events;
using MassTransit;
using System.Threading.Tasks;

namespace NotificationService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var message = context.Message;
            Console.WriteLine($"[NotificationService] Yeni sipariş alındı: {message.OrderId}, {message.CustomerName}");

            // Burada e-posta / SMS / push notification tetiklenebilir
            await Task.CompletedTask;
        }
    }
}
