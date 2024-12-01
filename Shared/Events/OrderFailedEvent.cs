using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class OrderFailedEvent : BaseCorrelation
    {
        public OrderFailedEvent() { }
        public OrderFailedEvent(Guid id) : base(id) { }

        public int OrderId { get; set; }    
        public String Message { get; set; }
    }
}
