using Rebus.Sagas;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Orchestration.Service.SagaData
{
    public class OrderSagaData : BaseCorrelation, ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        public int OrderId { get; set; }
        public int ConsumerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
