using Microsoft.Extensions.Logging;
using ProductListManagement.Data.Contracts;
using ProductListManagement.Data.Repositories.Contracts;
using ProductListManagement.Service.Contracts;
using ProductListManagement.Service.Dtos;
using ProductListManagement.Service.Mappers.Contracts;
using ProductListManagement.Services;

namespace ProductListManagement.Service
{
    public class ProductService : AbstractService, IProductService
    {
        private readonly IFilterService _filterService;

        public ProductService(ILogger<ProductService> logger, IMapperFactory mapperFactory, IDataContextManager dataContextManager, IFilterService filterService)
            : base(logger, mapperFactory, dataContextManager)
        {
            _filterService = filterService;
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var productRepository = DataContextManager.CreateRepository<IProductRepositoty>();
            var productMapper = MapperFactory.GetMapper<IProductMapper>();

            var product = productMapper.MapFromDto(productDto);
            await productRepository.AddAssync(product);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var productRepository = DataContextManager.CreateRepository<IProductRepositoty>();
            var productMapper = MapperFactory.GetMapper<IProductMapper>();

            var product = await productRepository.GetAsync(id);
            var productDto = productMapper.MapToDto(product);

            return productDto;
        }

        public async Task<IEnumerable<ProductShortDto>> GetAsync(FilterDto filter)
        {
            var productRepository = DataContextManager.CreateRepository<IProductRepositoty>();
            var productMapper = MapperFactory.GetMapper<IProductMapper>();

            var predicate = await _filterService.CreateProductFilter(filter);
            var products = await productRepository.GetAsync(predicate);
            var productShortDto = productMapper.MapCollectionToShortDto(products);

            return productShortDto;
        }

        public async Task<ProductDto> UpdateAsync(ProductDto productDto)
        {
            var productRepository = DataContextManager.CreateRepository<IProductRepositoty>();
            var productMapper = MapperFactory.GetMapper<IProductMapper>();

            var product = await productRepository.GetAsync(productDto.Id);
            productMapper.MapFromDto(productDto, destination: product);
            await DataContextManager.SaveAsync();

            return productDto;
        }
    }
}
