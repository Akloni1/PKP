using MediatR;

namespace PKP_Project.Application.Mediatr
{
    public class CancelAddDishIntoOrderComand : IRequest
    {
        public string? Dish { get; set; }
    }

    public class CancelAddDishIntoOrderComandHandler : IRequestHandler<CancelAddDishIntoOrderComand>
    {
        public Task Handle(CancelAddDishIntoOrderComand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Отмена");
            return Task.CompletedTask;
        }
    }
}
