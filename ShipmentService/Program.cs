
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shared;
using Shared.RabbitMqSettings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddRebus(rebus => rebus
     .Routing(r => r.TypeBased().MapAssemblyOf<BaseCorrelation>(RabbitMQQueueSettings.SagaOrchestrationInputQueueName))
     .Transport(t => t.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"), inputQueueName: RabbitMQQueueSettings.ShipmentInputQueueName)));

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var host = builder.Build();
host.Run();
