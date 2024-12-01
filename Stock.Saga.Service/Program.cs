using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shared;
using Shared.RabbitMqSettings;
using Stock.Saga.Service.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ServiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddRebus(rebus => rebus
     .Routing(r => r.TypeBased().MapAssemblyOf<BaseCorrelation>(RabbitMQQueueSettings.SagaOrchestrationInputQueueName))
     .Transport(t => t.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"), inputQueueName: RabbitMQQueueSettings.StockInputQueueName)));

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();


var host = builder.Build();
host.Run();
