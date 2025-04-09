using AddProduct_MongoDb;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Services.AddSingleton<IMongoClient>(
    s => new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection")));

builder.Build().Run();
