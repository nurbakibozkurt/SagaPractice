using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMqSettings
{
    public static class RabbitMQQueueSettings
    {
        public const string SagaOrchestrationInputQueueName = "saga-orchestration-input-queue-name";
        public const string OrderInputQueueName = "order-input-queue-name";
        public const string StockInputQueueName = "stock-input-queue-name";
        public const string PaymentInputQueueName = "payment-input-queue-name";
        public const string ShipmentInputQueueName = "shipment-input-queue-name";
    }
}
