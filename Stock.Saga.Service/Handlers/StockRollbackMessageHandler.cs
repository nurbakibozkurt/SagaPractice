using Microsoft.EntityFrameworkCore;
using Rebus.Handlers;
using Shared.Models;
using Shared.RollbackMessages;
using Stock.Saga.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Saga.Service.Handlers
{
    public class StockRollbackMessageHandler(ServiceDbContext serviceDbContext) : IHandleMessages<StockRollbackMessage>
    {
        public async Task Handle(StockRollbackMessage message)
        {
            foreach (OrderItemModel orderItem in message.OrderItems)
            {
                var stockItem = await serviceDbContext.Stocks.Where(item => item.ProductId == orderItem.ProductId).FirstOrDefaultAsync();
                if (stockItem != null)
                {
                    stockItem.Quantity += orderItem.Quantity;
                    serviceDbContext.Stocks.Update(stockItem);
                    await serviceDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
