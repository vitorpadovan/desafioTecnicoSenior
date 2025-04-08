using Bff.Controllers.Requests.Product;
using Bff.Controllers.Requests.Reseller;
using Bff.Controllers.Response.Reseller;
using Bogus;
using Bogus.Extensions.Brazil;
using System.Net;

namespace Challenge.IntegrationTest.Controllers
{
    public class ProductControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public ProductControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
            _faker = new Faker("pt_BR"); // Configura o Faker para o Brasil
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreated()
        {
            // Arrange
            var newResellerRequest = new NewResellerRequest
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
            var responseResellerCreate = await _client.PostAsJsonAsync("/api/reseller", newResellerRequest);
            var resellerId = await responseResellerCreate.Content.ReadFromJsonAsync<ResellerResponse>();
            var request = new NewProductRequest
            {
                Name = _faker.Commerce.ProductName(),
                Price = _faker.Random.Decimal(10.0m, 1000.0m)
            };

            // Act
            var response = await _client.PostAsJsonAsync($"/api/product/{resellerId!.Id}", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnNotFound()
        {
            // Arrange
            var resellerId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/product/{resellerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
