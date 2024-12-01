using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservationFailedEvent : BaseCorrelation
    {
        public StockReservationFailedEvent() { }
        public StockReservationFailedEvent(Guid id) : base(id){ }
        public string Message { get; set; }
    }
}
