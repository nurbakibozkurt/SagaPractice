using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservationSuccessEvent : BaseCorrelation
    {
        public StockReservationSuccessEvent() { }
        public StockReservationSuccessEvent(Guid id) : base(id) { }
       
    }
}
