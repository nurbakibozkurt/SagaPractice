using Payment.Service.Data;
using Rebus.Bus;
using Rebus.Handlers;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Service.Handlers
{
    public class PaymentStartedEventHandler (ServiceDbContext serviceDbContext, IBus rebus): IHandleMessages<PaymentStartedEvent>
    {
        public async Task Handle(PaymentStartedEvent message)
        {
            var costumer = await serviceDbContext.PaymentTest.FindAsync(message.ConsumerId);
            if ((costumer != null) && (costumer.CardBalance >= message.TotalPrice))
            {
                costumer.CardBalance -= message.TotalPrice;
                serviceDbContext.Update(costumer);
                await serviceDbContext.SaveChangesAsync();

                PaymentSuccessEvent paymentSuccessEvent = new(message.CorrelationId);
                await rebus.Send(paymentSuccessEvent);
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new(message.CorrelationId)
                {
                    Message = "Insufficient Balance."
                };
                await rebus.Send(paymentFailedEvent);
            }
        }
    }
}
