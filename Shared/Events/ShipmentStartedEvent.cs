using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class ShipmentStartedEvent : BaseCorrelation
    {
        public ShipmentStartedEvent() { }
        public ShipmentStartedEvent(Guid id) : base(id) { }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
