using Order.Saga.Service.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga.Service.Models
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
