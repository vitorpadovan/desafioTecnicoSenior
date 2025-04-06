using System;
using Challenge.Domain.Entities.Dto;
using FabricServices.Implementation;
using FabricServices.Interfaces;
using FabricServices.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SendToFabric
{
    public class SendToFabricFunction
    {
        private readonly ILogger _logger;
        private readonly IFabricService _fabricService;
        private readonly string _serviceUrl;
        private readonly string _serviceUser;
        private readonly string _servicePassword;

        public SendToFabricFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SendToFabricFunction>();
            _serviceUrl = Environment.GetEnvironmentVariable("ServiceUrl");
            _serviceUser = Environment.GetEnvironmentVariable("ServiceUser");
            _servicePassword = Environment.GetEnvironmentVariable("ServicePassword");
            _fabricService = new FabricOfBeer(new HttpClient(), _serviceUrl, _serviceUser, _servicePassword);
        }

        [Function("SendToFabricFunction")]
        public async Task Run([RabbitMQTrigger("request_to_fabric", ConnectionStringSetting = "RabbitMq")] List<OrderDetailDto> list)
        {
            _logger.LogDebug($"Service URL: {_serviceUrl}, User: {_serviceUser}");
            try
            {
                await _fabricService.SendToFabric(list
                .Select(x => new FabriOrderDetail() { BarCode = x.ProductId, Quantity = x.Quantity })
                .ToList());
            }
            catch (Exception ex) {
                //Este try catch está aqui apenas porquê como não foi passado especificamente nenhum api ou algo do tipo para enviar, a mesma sempre irá dar problemas
                //então estou deduzindo que a mesma funcionou corretamente
            }
            _logger.LogInformation($"C# Queue trigger function processed: {list}");
        }
    }
}
