using Challenge.Orm;
using Testcontainers.PostgreSql;


namespace Challenge.IntegrationTest
{
    public class IntegrationTestFixture : IDisposable
    {
        public HttpClient Client { get; }
        private readonly AppTesteServer _testeServer;
        private readonly AppDbContext _appDbContext;
        private readonly PostgreSqlContainer _dbContainer;

        public IntegrationTestFixture()
        {
            _dbContainer = new PostgreSqlBuilder()
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .Build();
            _dbContainer.StartAsync().GetAwaiter().GetResult();
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
                            { "ConnectionStrings:PostgresSqlConnectionString", _dbContainer.GetConnectionString() }
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
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
            Client.Dispose();
            _testeServer.Dispose();
        }
    }
}
