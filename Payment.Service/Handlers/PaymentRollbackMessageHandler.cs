using Payment.Service.Data;
using Rebus.Bus;
using Rebus.Handlers;
using Shared.RollbackMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.Handlers
{
    public class PaymentRollbackMessageHandler(ServiceDbContext serviceDbContext, IBus rebus) : IHandleMessages<PaymentRollbackMessage>
    {
        public async Task Handle(PaymentRollbackMessage message)
        {
            var costumer = await serviceDbContext.PaymentTest.FindAsync(message.ConsumerId);
            if (costumer != null)
            {
                costumer.CardBalance += message.TotalPrice;
                serviceDbContext.Update(costumer);
                await serviceDbContext.SaveChangesAsync();
            }
        }
    }
}
