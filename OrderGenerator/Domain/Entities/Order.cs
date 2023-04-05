namespace OrderGenerator.Domain.Entities
{
    public class Order
    {
        public string Symbol { get; set; }
        public bool IsBuy { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
