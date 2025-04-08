using System.Net;
using Bff.Controllers.Requests.Order;
using Bff.Controllers.Requests.Product;
using Bff.Controllers.Requests.Reseller;
using Bff.Controllers.Response.Product;
using Bff.Controllers.Response.Reseller;
using Bogus;
using Bogus.Extensions.Brazil;
using Challenge.Domain.Entities;

namespace Challenge.IntegrationTest.Controllers
{
    public class OrderControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;
        private readonly IntegrationTestFixture _fixture;

        public OrderControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
            _faker = new Faker("pt_BR"); // Configura o Faker para o Brasil
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreated()
        {
            // Arrange
            #region Create Reseller
            var requestCreateReseller = new NewResellerRequest
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
            var responseCreateReseller = await _client.PostAsJsonAsync("/api/reseller", requestCreateReseller);
            var resellerResponse = await responseCreateReseller.Content.ReadFromJsonAsync<ResellerResponse>();
            #endregion

            #region Create Product
            var requestNewProduct = new NewProductRequest
            {
                Name = _faker.Commerce.ProductName(),
                Price = _faker.Random.Decimal(10.0m, 1000.0m)
            };
            var productCreateResponse = await _client.PostAsJsonAsync($"/api/product/{resellerResponse!.Id}", requestNewProduct);
            var productResponse = await productCreateResponse.Content.ReadFromJsonAsync<ProductResponse>();
            #endregion

            var request = new NewOrderRequest
            {
                OrderDetails = new List<NewOrderDetailRequest>
                {
                    new NewOrderDetailRequest 
                    { 
                        ProductId = productResponse!.Id, 
                        Quantity = _faker.Random.Int(1, 10) 
                    }
                }
            };
            var token = await _fixture.LoginAs(_fixture._adminUser);
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);  


            // Act
            var response = await _client.PostAsJsonAsync($"/api/order/{resellerResponse!.Id}", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
