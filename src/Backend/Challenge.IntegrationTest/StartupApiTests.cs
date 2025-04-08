using Bff; // Certifique-se de que o namespace correto está sendo importado
using Bff.Controllers.Filters;
using Bff.Extensions;
using Challenge.Orm;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace Challenge.IntegrationTest
{
    public class StartupApiTests
    {
        private readonly IConfiguration _configuration;

        public StartupApiTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connString = _configuration.GetConnectionString("PostgresSqlConnectionString");
            ArgumentNullException.ThrowIfNullOrEmpty(connString, nameof(connString));
            services.AddControllers(options => options.Filters.Add<HttpResponseExceptionFilter>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "Bff"));
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    connString,
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
            var authKey = _configuration["AuthKey"];
            var key = Encoding.ASCII.GetBytes(authKey!);
            services.AddAuthentication(x =>
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
            services.AddDefaultIdentity<IdentityUser>(opt =>
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
            services.AddDependencyInjections();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
