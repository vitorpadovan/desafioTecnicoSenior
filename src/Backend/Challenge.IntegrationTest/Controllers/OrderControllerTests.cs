using Bff.Controllers.Requests.Order;
using Bogus;
using System.Net;

namespace Challenge.IntegrationTest.Controllers
{
    public class OrderControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public OrderControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
            _faker = new Faker("pt_BR"); // Configura o Faker para o Brasil
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreated()
        {
            // Arrange
            var resellerId = Guid.NewGuid();
            var request = new NewOrderRequest
            {
                OrderDetails = new List<NewOrderDetailRequest>
                {
                    new NewOrderDetailRequest
                    {
                        ProductId = _faker.Random.Int(1, 100),
                        Quantity = _faker.Random.Int(1, 10)
                    }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync($"/api/order/{resellerId}", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
