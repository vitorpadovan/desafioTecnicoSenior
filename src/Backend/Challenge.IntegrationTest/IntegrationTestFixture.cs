using Bff.Controllers.Requests.User;
using Bogus;
using Challenge.Orm;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.Data;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;


namespace Challenge.IntegrationTest
{
    public class IntegrationTestFixture : IDisposable
    {
        public HttpClient Client { get; }
        private readonly AppTesteServer _testeServer;
        private readonly AppDbContext _appDbContext;
        private readonly PostgreSqlContainer _dbContainer;
        private readonly RabbitMqContainer _rabbitMqContainer;
        private readonly Faker _faker = new("pt_BR");
        public readonly NewUserRegisterRequest _adminUser;

        public IntegrationTestFixture()
        {
            _dbContainer = new PostgreSqlBuilder()
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .Build();
            _rabbitMqContainer = new RabbitMqBuilder()
                .Build();
            _dbContainer.StartAsync().GetAwaiter().GetResult();
            _rabbitMqContainer.StartAsync().GetAwaiter().GetResult();

            IWebHostBuilder builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    var environment = context.HostingEnvironment.EnvironmentName;
                    var appSettings = $"appsettings.{environment}.json";
                    configurationBuilder
                        .AddJsonFile(appSettings, optional: false)
                        .AddEnvironmentVariables()
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { "ConnectionStrings:PostgresSqlConnectionString", _dbContainer.GetConnectionString() },
                            { "ConnectionStrings:RabbitMq", _rabbitMqContainer.GetConnectionString() }
                        });
                })
                .UseStartup<StartupApiTests>();
            _testeServer = new AppTesteServer(builder);
            _appDbContext = _testeServer.Services.GetRequiredService<AppDbContext>();
            try
            {
                _appDbContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating database", ex);
            }
            Client = _testeServer.CreateClient();
            _adminUser = new()
            {
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };
            CreateUser().GetAwaiter().GetResult();
        }

        private async Task CreateUser()
        {
            var response = await Client.PostAsJsonAsync("/api/user/create-admin", _adminUser);
        }

        public async Task<AccessTokenResponse> LoginAs(NewUserRegisterRequest asd)
        {
            var loginRequest = new LoginRequest
            {
                Email = asd.Email,
                Password = asd.Password
            };
            var response = await Client.PostAsJsonAsync("/api/user/login", loginRequest);
            var token = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
            return token;
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
            Client.Dispose();
            _testeServer.Dispose();
        }
    }
}
