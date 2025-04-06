using FabricServices.Interfaces;
using FabricServices.Model;
using Polly;
using Polly.Retry;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FabricServices.Implementation
{
    public class FabricOfBeer : IFabricService
    {
        private readonly HttpClient _httpClient;
        private readonly string _fabricApiUrl;
        private readonly string _username;
        private readonly string _password;
        private string _jwtToken;

        public FabricOfBeer(HttpClient httpClient, string fabricApiUrl, string username, string password)
        {
            _httpClient = httpClient;
            _fabricApiUrl = fabricApiUrl;
            _username = username;
            _password = password;
        }

        public async Task<FabricOrder> SendToFabric(List<FabriOrderDetail> details)
        {
            if (string.IsNullOrEmpty(_jwtToken))
            {
                _jwtToken = await AuthenticateAsync();
            }

            var retryPolicy = CreateRetryPolicy();

            return await retryPolicy.ExecuteAsync(async () =>
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_fabricApiUrl}/orders")
                {
                    Content = new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, "application/json")
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Reauthenticate and retry
                    _jwtToken = await AuthenticateAsync();
                    throw new HttpRequestException("Token expired. Retrying with a new token.");
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to send order. Status code: {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<FabricOrder>(responseContent);
            });
        }

        private async Task<string> AuthenticateAsync()
        {
            var authRequest = new HttpRequestMessage(HttpMethod.Post, $"{_fabricApiUrl}/auth")
            {
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    Username = _username,
                    Password = _password
                }), Encoding.UTF8, "application/json")
            };

            var authResponse = await _httpClient.SendAsync(authRequest);

            if (!authResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Authentication failed. Status code: {authResponse.StatusCode}");
            }

            var authContent = await authResponse.Content.ReadAsStringAsync();
            var authResult = JsonSerializer.Deserialize<AuthResponse>(authContent);

            return authResult?.Token ?? throw new InvalidOperationException("Authentication token is missing.");
        }

        private static AsyncRetryPolicy CreateRetryPolicy()
        {
            return Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 5),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to {exception.Message}");
                    });
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
}
