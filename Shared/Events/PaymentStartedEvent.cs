using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentStartedEvent : BaseCorrelation
    {
        public PaymentStartedEvent() { }
        public PaymentStartedEvent(Guid id) : base(id) { }
        public int ConsumerId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
