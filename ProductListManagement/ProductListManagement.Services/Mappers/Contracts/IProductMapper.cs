using ProductListManagement.Model;
using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Service.Mappers.Contracts
{
    public interface IProductMapper : IMapper<Product, ProductDto>
    {
        IEnumerable<ProductShortDto> MapCollectionToShortDto(IEnumerable<Product> products);
        ProductShortDto MapToShortDto(Product products);
    }
}
