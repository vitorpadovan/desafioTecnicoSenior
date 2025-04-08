using System.Net;
using Bff.Controllers.Requests.Order;

namespace Challenge.IntegrationTest.Controllers
{
    public class OrderControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;

        public OrderControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
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
                    new NewOrderDetailRequest { ProductId = 1, Quantity = 2 }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync($"/api/order/{resellerId}", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
