using Challenge.Application.Business;
using Challenge.Domain.Business;
using Challenge.Domain.Repositories;
using Challenge.Orm;
using Challenge.Orm.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Bff", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresSqlConnectionString"),
        b => b.MigrationsAssembly("Challenge.Orm")
    )
);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IResellerBusiness, ResellerBusiness>();
builder.Services.AddScoped<IResellerRepository, ResellerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(x => { });

app.Run();
