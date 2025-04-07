using Bff.Controllers.Filters;
using Bff.Extensions;
using Challenge.Common.Interfaces;
using Challenge.Orm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<HttpResponseExceptionFilter>());
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Bff", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
var authKey = builder.Configuration[$"AuthKey"];
var key = Encoding.ASCII.GetBytes(authKey!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddDefaultIdentity<IdentityUser>(opt =>
    {
        // Password settings.
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireNonAlphanumeric = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequiredLength = 5;
        opt.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opt.Lockout.MaxFailedAccessAttempts = 5;
        opt.Lockout.AllowedForNewUsers = true;

        // User settings.
        opt.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#!";
        opt.User.RequireUniqueEmail = true;

        // SignIn settings.
        opt.SignIn.RequireConfirmedAccount = true;
        opt.SignIn.RequireConfirmedEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDependencyInjections();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Services.InitDatabase();
using (var scope = app.Services.CreateScope())
{
    var rabbitMq = scope.ServiceProvider.GetRequiredService<IMessageService>();
    rabbitMq.InitQueueAsync("order-recived").Wait();
    rabbitMq.InitQueueAsync("request_to_fabric").Wait();
}
app.Run();
