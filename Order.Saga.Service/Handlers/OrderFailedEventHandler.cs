using Order.Saga.Service.Data;
using Rebus.Handlers;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Saga.Service.Handlers
{
    public class OrderFailedEventHandler(AppDbContext appDbContext) : IHandleMessages<OrderFailedEvent>
    {
        public async Task Handle(OrderFailedEvent message)
        {
            var order = await appDbContext.Orders.FindAsync(message.OrderId);
            if (order != null)
            {
                order.OrderStatus = Models.Enums.OrderStatus.Failed;
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
