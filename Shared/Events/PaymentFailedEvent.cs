using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentFailedEvent : BaseCorrelation
    {
        public PaymentFailedEvent() { }
        public PaymentFailedEvent(Guid id) : base(id) { }  
        public string Message { get; set; }

    }
}
