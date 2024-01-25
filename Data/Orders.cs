using PKP_Project.Domain.AggregationModels;

namespace PKP_Project.Data
{
    public class Orders
    {
        public IList<Order> OrderList = new List<Order>
            {
               new Order(1,  new List<string>(){} ,"Пустой"),
               new Order(2,  new List<string>(){"Борщ","Котлета с рисом","Компот"} ,"Бизнес Ланч"),
               new Order(3,  new List<string>(){"Омлет"} ,"Завтрак"),
            };
    }
}
