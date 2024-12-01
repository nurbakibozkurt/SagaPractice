using Rebus.Bus;
using Rebus.Handlers;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.Handlers
{
    public class ShipmentStartedEventHandler(IBus rebus) : IHandleMessages<ShipmentStartedEvent>
    {
        public async Task Handle(ShipmentStartedEvent message)
        {
            // Sagayı test edebilmek amacıyla eğer siparişte 4'ten fazla farklı türde ürün
            // ile sipariş verilmişse kargo işlemi başarız sayılacaktır.
            if (message.OrderItems.Count <= 4)
            {
                await rebus.Send(new ShipmentSuccessEvent(message.CorrelationId));
            }
            else
            {
                await rebus.Send(new ShipmentFailedEvent(message.CorrelationId)
                {
                    Message = "Shipment Failed."
                });
            }
        }
    }
}
