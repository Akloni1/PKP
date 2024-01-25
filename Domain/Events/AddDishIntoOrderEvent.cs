using MediatR;
using PKP_Project.Domain.AggregationModels;

namespace PKP_Project.Domain.Events
{
    public class AddDishIntoOrderEvent : INotification
    {
        public long Id { get; set; }
        public string Dish { get; set; }
        public string Type { get; set; }
        public AddDishIntoOrderEvent(long id, string dish, string type)
        {
            Id = id;
            Dish = dish;
            Type = type;    
        }
    }
}
