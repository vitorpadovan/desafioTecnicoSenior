using AutoMapper;
using Challenge.Common.Interfaces;
using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Entities.Dto;
using Challenge.Domain.Repositories;

namespace Challenge.Application.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        private readonly IResellerRepository _resellerRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ProductBusiness(IProductRepository productRepository, IResellerRepository resellerRepository, IMessageService messageService, IMapper mapper)
        {
            _productRepository = productRepository;
            _resellerRepository = resellerRepository;
            _messageService = messageService;
            _mapper = mapper;
        }

        public Task<Product> GetProductAsync(Guid resellerId, int productId, bool AsNonTracking = true)
        {
            return _productRepository.GetProductAsync(resellerId, productId);
        }

        public Task<List<Product>> GetProductsAsync(Guid resellerId, bool AsNonTracking = true)
        {
            return _productRepository.GetProductsAsync(resellerId, AsNonTracking);
        }

        public async Task<Product> SaveProductAsync(Guid id, Product request)
        {
            var reseller = await _resellerRepository.GetResellerByIdAsync(id, false);
            request.Reseller = reseller;
            var @return = await _productRepository.SaveProductAsync(request);
            var requestDto = _mapper.Map<ProductDto>(request);
            await _messageService.PublishAsync(requestDto, exchange: "add_product", routingKey: "add_product");
            return @return;
        }
    }
}
