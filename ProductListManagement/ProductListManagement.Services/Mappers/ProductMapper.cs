using AutoMapper;
using ProductListManagement.Model;
using ProductListManagement.Service.Dtos;
using ProductListManagement.Service.Mappers.Contracts;

namespace ProductListManagement.Service.Mappers
{
    public class ProductMapper : AbstractMapper<Product, ProductDto>, IProductMapper
    {
        public IEnumerable<ProductShortDto> MapCollectionToShortDto(IEnumerable<Product> products)
        {
            return products.Select(p => MapToShortDto(p));
        }

        public ProductShortDto MapToShortDto(Product products)
        {
            return Mapper.Map<ProductShortDto>(products);
        }

        protected override AutoMapper.IMapper Configure()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<Product, ProductShortDto>();
            });
            return config.CreateMapper();
        }
    }
}
