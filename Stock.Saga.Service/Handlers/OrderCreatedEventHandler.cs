using Microsoft.EntityFrameworkCore;
using Rebus.Bus;
using Rebus.Handlers;
using Shared.Events;
using Stock.Saga.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Saga.Service.Handlers
{
    public class OrderCreatedEventHandler(ServiceDbContext serviceDbContext, IBus rebus) : IHandleMessages<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent message)
        {
            bool isAllItemsAvaliable = true;

            foreach (var orderItem in message.OrderItems) {
                if(!await serviceDbContext.Stocks.Where(item => item.ProductId == orderItem.ProductId).Where(item => item.Quantity >= orderItem.Quantity).AnyAsync())
                {
                    isAllItemsAvaliable = false;
                }
            }

            if (isAllItemsAvaliable)
            {
                foreach (var orderItem in message.OrderItems)
                {
                    var stockItem = await serviceDbContext.Stocks.Where(item => item.ProductId == orderItem.ProductId).FirstOrDefaultAsync();
                    if (stockItem != null)
                    {
                        stockItem.Quantity -= orderItem.Quantity;
                        serviceDbContext.Stocks.Update(stockItem);
                        await serviceDbContext.SaveChangesAsync();
                    }
                }

                StockReservationSuccessEvent stockReservationSuccessEvent = new(message.CorrelationId);
                await rebus.Send(stockReservationSuccessEvent);
            }
            else
            {
                StockReservationFailedEvent stockReservationFailedEvent = new(message.CorrelationId)
                {
                    Message = "Out of Stock."
                };

                await rebus.Send(stockReservationFailedEvent);
            }
            
        }
    }
}
