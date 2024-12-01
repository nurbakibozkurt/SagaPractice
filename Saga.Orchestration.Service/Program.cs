
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shared.Events;
using Shared.RabbitMqSettings;
using Shared.RollbackMessages;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddRebus(rebus => rebus
     .Routing(r =>
        r.TypeBased()
        .Map<OrderCreatedEvent>(RabbitMQQueueSettings.StockInputQueueName)
        .Map<PaymentStartedEvent>(RabbitMQQueueSettings.PaymentInputQueueName)
        .Map<OrderFailedEvent>(RabbitMQQueueSettings.OrderInputQueueName)
        .Map<OrderCompletedEvent>(RabbitMQQueueSettings.OrderInputQueueName)
        .Map<ShipmentStartedEvent>(RabbitMQQueueSettings.ShipmentInputQueueName)
        .Map<StockRollbackMessage>(RabbitMQQueueSettings.StockInputQueueName)
        .Map<PaymentRollbackMessage>(RabbitMQQueueSettings.PaymentInputQueueName)
        )
     .Transport(t =>
         t.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"),
         inputQueueName: RabbitMQQueueSettings.SagaOrchestrationInputQueueName))
     .Sagas(s =>
         s.StoreInSqlServer(builder.Configuration.GetConnectionString("SQLServer"),
         dataTableName: "Sagas",
         indexTableName: "SagaIndexes",
         automaticallyCreateTables: true))
     .Timeouts(t =>
         t.StoreInSqlServer(builder.Configuration.GetConnectionString("SQLServer"),
         tableName: "Timeouts",
         automaticallyCreateTables: true)));

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var host = builder.Build();
host.Run();
