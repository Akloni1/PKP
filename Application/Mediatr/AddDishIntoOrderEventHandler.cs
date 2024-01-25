using Confluent.Kafka;
using MediatR;
using PKP_Project.Domain.Events;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace PKP_Project.Application.Mediatr
{
    public class AddDishIntoOrderEventHandler : INotificationHandler<AddDishIntoOrderEvent>
    {
        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "training-kafka";
        public async Task Handle(AddDishIntoOrderEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Произошло событие добавления нового блюда {notification.Dish}");
            var message = JsonSerializer.Serialize(notification);
            await SendOrderRequest(topic, message);
        }


        private async Task<bool> SendOrderRequest(string topic, string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using var producer = new ProducerBuilder<Null, string>(config).Build();

                var result = await producer.ProduceAsync(topic, new Message<Null, string>
                {
                    Value = message
                });

                Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
