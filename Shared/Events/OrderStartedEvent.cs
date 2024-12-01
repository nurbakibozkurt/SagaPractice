using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class OrderStartedEvent : BaseCorrelation
    {
        public OrderStartedEvent() { }
        public OrderStartedEvent(Guid id) : base(id) { }
        public int OrderId { get; set; }
        public int ConsumerId { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
