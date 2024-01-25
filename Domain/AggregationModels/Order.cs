namespace PKP_Project.Domain.AggregationModels
{
    public class Order
    {
        public Order() { }
        public Order(long id, ICollection<string>? dishes, string? type)
        {
            Id = id;
            Dishes = dishes;
            Type = type;
        }
        public long Id { get; }
        public ICollection<string>? Dishes { get; }
        public string? Type { get; }
    }
}
