using Order.API.Models;

namespace Order.API.Dtos
{
    public class OrderModelDto
    {
        public int ConsumerId { get; set; }
        public List<OrderItemModelDto> OrderItems { get; set; }
    }
}
