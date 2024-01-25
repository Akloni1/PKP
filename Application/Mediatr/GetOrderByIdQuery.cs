using Confluent.Kafka;
using MediatR;
using PKP_Project.Data;
using PKP_Project.Domain.AggregationModels;
using PKP_Project.Domain.Events;
using System.Text.Json;

namespace PKP_Project.Application.Mediatr
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public long Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        const string topic = "training-kafka";
        const string groupId = "test_group";
        const string bootstrapServers = "localhost:9092";

        ConsumerConfig config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,

        };
        private Orders _orders;
        public GetOrderByIdQueryHandler()
        {
            _orders = new Orders();
        }
        public Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orders.OrderList.Where(x => x.Id == request.Id).FirstOrDefault();
            using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                try
                {
                    consumerBuilder.Subscribe(topic);
                    var consumer = consumerBuilder.Consume(cancellationToken);
                    while (consumer != null)
                    {
                        if (consumer.Message.Value != null)
                        {
                            var dish = JsonSerializer.Deserialize<AddDishIntoOrderEvent>(consumer.Message.Value) ?? throw new Exception();
                            if (dish.Id == request.Id)
                            {
                                if(dish?.Type?.ToLower() == "add") 
                                   order?.Dishes?.Add(dish.Dish);

                                if (dish?.Type?.ToLower() == "cancel")
                                    order?.Dishes?.Remove(dish.Dish);
                            }
                        }
                        consumer = consumerBuilder.Consume(100);
                    }
                }
                finally
                {
                    consumerBuilder.Close();
                }
            }
            return Task.FromResult(order);
        }
    }
}
