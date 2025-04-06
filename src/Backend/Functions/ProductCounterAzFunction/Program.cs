using Challenge.Common.Implementation;
using Challenge.Common.Interfaces;
using Challenge.Orm;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddScoped<ICacheService, RedisService>();
builder.Services.AddScoped<IMessageService, RabbitMqService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("RedisServer");
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresSqlConnectionString"),
        plSqlOpt =>
        {
            plSqlOpt.MigrationsAssembly("Challenge.Orm");
            plSqlOpt.CommandTimeout(60);
            plSqlOpt.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null
            );
        }
    )
);

builder.Build().Run();
