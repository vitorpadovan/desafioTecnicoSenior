using System.Net;
using System.Net.Http.Json;
using Bff.Controllers.Requests.Product;
using Xunit;

namespace Challenge.IntegrationTest.Controllers
{
    public class ProductControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(IntegrationTestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreated()
        {
            // Arrange
            var resellerId = Guid.NewGuid();
            var request = new NewProductRequest
            {
                Name = "Test Product",
                Price = 100.0m
            };

            // Act
            var response = await _client.PostAsJsonAsync($"/api/product/{resellerId}", request);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk()
        {
            // Arrange
            var resellerId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/product/{resellerId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
