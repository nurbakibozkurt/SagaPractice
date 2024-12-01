using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using Saga.Orchestration.Service.SagaData;
using Shared.Events;
using Shared.RollbackMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saga.Orchestration.Service
{
    public class OrderSaga(IBus rebus) :
        Saga<OrderSagaData>,
        IAmInitiatedBy<OrderStartedEvent>,
        IHandleMessages<StockReservationSuccessEvent>,
        IHandleMessages<StockReservationFailedEvent>,
        IHandleMessages<PaymentSuccessEvent>,
        IHandleMessages<PaymentFailedEvent>,
        IHandleMessages<ShipmentSuccessEvent>,
        IHandleMessages<ShipmentFailedEvent>
    {
        public async Task Handle(OrderStartedEvent message)
        {
            if (!IsNew)
            {
                return;
            }

            Data.ConsumerId = message.ConsumerId;
            Data.OrderId = message.OrderId;
            Data.OrderItems = message.OrderItems;
            Data.TotalPrice = message.TotalPrice;

            await rebus.Send(new OrderCreatedEvent(message.CorrelationId)
            {
                OrderItems = message.OrderItems
            });
        }


        public async Task Handle(StockReservationSuccessEvent message)
        {
            await rebus.Send(new PaymentStartedEvent(message.CorrelationId)
            {
                ConsumerId = Data.ConsumerId,
                TotalPrice = Data.TotalPrice,
            });
        }

        public async Task Handle(StockReservationFailedEvent message)
        {
            await rebus.Send(new OrderFailedEvent(message.CorrelationId)
            {
                OrderId = Data.OrderId,
                Message = message.Message,
            });
        }


        public async Task Handle(PaymentSuccessEvent message)
        {
            await rebus.Send(new ShipmentStartedEvent(message.CorrelationId)
            {
                OrderItems = Data.OrderItems
            });
        }

        public async Task Handle(PaymentFailedEvent message)
        {
            await rebus.Send(new OrderFailedEvent(message.CorrelationId)
            {
                OrderId = Data.OrderId,
                Message = message.Message,
            });

            await rebus.Send(new StockRollbackMessage(message.CorrelationId)
            {
                OrderItems = Data.OrderItems
            });
        }
        public async Task Handle(ShipmentSuccessEvent message)
        {
            await rebus.Send(new OrderCompletedEvent(){
                OrderId = Data.OrderId,
            });

            MarkAsComplete();
        }

        public async Task Handle(ShipmentFailedEvent message)
        {
            await rebus.Send(new OrderFailedEvent(message.CorrelationId)
            {
                OrderId = Data.OrderId,
                Message = message.Message,
            });

            await rebus.Send(new PaymentRollbackMessage(message.CorrelationId)
            {
                ConsumerId = Data.ConsumerId,
                TotalPrice = Data.TotalPrice,
            });

            await rebus.Send(new StockRollbackMessage(message.CorrelationId)
            {
                OrderItems = Data.OrderItems
            });
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderSagaData> config)
        {
            config.Correlate<OrderStartedEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<StockReservationSuccessEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<StockReservationFailedEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<PaymentSuccessEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<PaymentFailedEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<ShipmentSuccessEvent>(m => m.CorrelationId, s => s.Id);

            config.Correlate<ShipmentFailedEvent>(m => m.CorrelationId, s => s.Id);
        }
    }
}
