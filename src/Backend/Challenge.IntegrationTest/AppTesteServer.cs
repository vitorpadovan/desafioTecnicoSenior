using Microsoft.AspNetCore.TestHost;

namespace Challenge.IntegrationTest
{
    public class AppTesteServer : TestServer, IDisposable
    {
        public AppTesteServer(IWebHostBuilder builder) : base(builder)
        {
        }
    }
}
