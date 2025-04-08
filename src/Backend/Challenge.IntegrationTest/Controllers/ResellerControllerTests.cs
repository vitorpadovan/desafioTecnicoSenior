using System.Net;
using System.Net.Http.Json;
using Bff.Controllers.Requests.Reseller;
using Xunit;

namespace Challenge.IntegrationTest.Controllers
{
    public class ResellerControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;

        public ResellerControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateReseller_ShouldReturnCreated()
        {
            // Arrange
            var request = new NewResellerRequest
            {
                Document = "77.034.148/0001-71",
                RegistredName = "Test Reseller",
                TradeName = "Test Trade",
                Email = "test@example.com",
                Addresses = new List<AddressRequest>
                {
                    new AddressRequest
                    {
                        City = "Test City",
                        Province = "Test Province",
                        Street = "Test Street",
                        Number = 123,
                        PostalCode = 123123,
                        Country = "Test Country"
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/reseller", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetReseller_ShouldReturnOk()
        {
            // Arrange
            var resellerId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/reseller/{resellerId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
