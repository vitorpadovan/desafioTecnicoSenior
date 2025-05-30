using System.Net;
using System.Net.Http.Json;
using Bff.Controllers.Requests.Reseller;
using Bff.Controllers.Response.Reseller;
using Bogus;
using Bogus.Extensions.Brazil;
using Challenge.Domain.Entities;
using Xunit;

namespace Challenge.IntegrationTest.Controllers
{
    public class ResellerControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;
        private readonly Faker _faker; 

        public ResellerControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
            _faker = new Faker("pt_BR"); // Configura o Faker para o Brasil
        }

        [Fact]
        public async Task CreateReseller_ShouldReturnCreated()
        {
            // Arrange
            var request = new NewResellerRequest
            {
                Document = _faker.Company.Cnpj(includeFormatSymbols: true),
                RegistredName = _faker.Company.CompanyName(),
                TradeName = _faker.Company.CompanySuffix(),
                Email = _faker.Internet.Email(),
                Addresses = new List<AddressRequest>
                {
                    new AddressRequest
                    {
                        City = _faker.Address.City(),
                        Province = _faker.Address.State(),
                        Street = _faker.Address.StreetName(),
                        Number = _faker.Random.Int(1, 1000),
                        PostalCode = _faker.Random.Int(100000, 999999),
                        Country = _faker.Address.Country()
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/reseller", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetReseller_ShouldReturnNotFound()
        {
            // Arrange
            var resellerId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/reseller/{resellerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetReseller_ShouldReturnOkAndReseller()
        {
            // Arrange
            var request = new NewResellerRequest
            {
                Document = _faker.Company.Cnpj(includeFormatSymbols: true),
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
            var createResponse = await _client.PostAsJsonAsync("/api/reseller", request);
            var ResellerResponse = await createResponse.Content.ReadFromJsonAsync<ResellerResponse>();

            // Act
            var response = await _client.GetAsync($"/api/reseller/{ResellerResponse!.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
