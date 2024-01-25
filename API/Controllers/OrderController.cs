using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKP_Project.Application.Mediatr;
using PKP_Project.Domain.AggregationModels;
using PKP_Project.Domain.Events;

namespace PKP_Project.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<Order> GetById(long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetOrderByIdQuery()
                {
                    Id = id
                },
                cancellationToken);
            return result;
        }

        [HttpPut]
        [Route("Add")]
        public async void AddDishIntoOrder(long id, string dish, string type, CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new AddDishIntoOrderComand()
                {
                    Dish = dish
                },
                cancellationToken);
           await _mediator.Publish(new AddDishIntoOrderEvent(id, dish, type), cancellationToken);
        }

        [HttpPut]
        [Route("Cancel")]
        public async void CancelAddDishIntoOrder(long id, string dish, string type, CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new CancelAddDishIntoOrderComand()
                {
                    Dish = dish
                },
                cancellationToken);
            await _mediator.Publish(new AddDishIntoOrderEvent(id, dish, type), cancellationToken);
        }
    }
}
