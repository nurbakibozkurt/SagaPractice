namespace Order.API.Dtos
{
    public class OrderItemModelDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
