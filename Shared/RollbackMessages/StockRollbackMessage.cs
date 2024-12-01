using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RollbackMessages
{
    public class StockRollbackMessage : BaseCorrelation
    {
        public StockRollbackMessage() { }
        public StockRollbackMessage(Guid id) : base(id) { }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
