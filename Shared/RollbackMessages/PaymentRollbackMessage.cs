using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RollbackMessages
{
    public class PaymentRollbackMessage : BaseCorrelation
    {
        public PaymentRollbackMessage() { }
        public PaymentRollbackMessage(Guid id) : base(id) { }
        public int ConsumerId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
