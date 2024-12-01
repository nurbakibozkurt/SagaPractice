using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class ShipmentFailedEvent : BaseCorrelation
    {
        public ShipmentFailedEvent() { }
        public ShipmentFailedEvent(Guid id) : base(id) { }
        public string Message { get; set; }

    }
}
