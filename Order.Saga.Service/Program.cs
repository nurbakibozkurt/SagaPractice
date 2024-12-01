using Microsoft.EntityFrameworkCore;
using Order.Saga.Service.Data;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shared;
using Shared.RabbitMqSettings;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddRebus(rebus => rebus
     .Routing(r => r.TypeBased().MapAssemblyOf<BaseCorrelation>(RabbitMQQueueSettings.SagaOrchestrationInputQueueName))
     .Transport(t => t.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"), inputQueueName: RabbitMQQueueSettings.OrderInputQueueName)));

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var host = builder.Build();
host.Run();
