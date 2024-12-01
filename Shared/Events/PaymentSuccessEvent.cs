using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentSuccessEvent : BaseCorrelation
    {
        public PaymentSuccessEvent() { }
        public PaymentSuccessEvent(Guid id) : base(id){ }

    }
}
