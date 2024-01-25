using Confluent.Kafka;
using MediatR;
using System.Text;
using static Confluent.Kafka.ConfigPropertyNames;

namespace PKP_Project.Application.Mediatr
{
    public class AddDishIntoOrderComand : IRequest
    {
        public string? Dish { get; set; }
    }

    public class AddDishComandHandler : IRequestHandler<AddDishIntoOrderComand>
    {
        public Task Handle(AddDishIntoOrderComand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Добавление");
            return Task.CompletedTask;
        }
    }
}
