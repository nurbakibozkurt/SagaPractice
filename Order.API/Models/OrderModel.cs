using Order.API.Models.Enums;

namespace Order.API.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int ConsumerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }


    }
}