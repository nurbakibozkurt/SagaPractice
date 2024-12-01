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
    public class OrderCompletedEventHandler (AppDbContext appDbContext) : IHandleMessages<OrderCompletedEvent>
    {
        public async Task Handle(OrderCompletedEvent message)
        {
            var order = await appDbContext.Orders.FindAsync(message.OrderId);
            if(order != null)
            {
                order.OrderStatus = Models.Enums.OrderStatus.Completed;
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
