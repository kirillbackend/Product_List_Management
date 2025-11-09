using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Service.Contracts
{
    public interface IProductService
    {
        Task AddAsync(ProductDto productDto);
        Task<ProductDto> GetAsync(Guid id);
        Task<IEnumerable<ProductShortDto>> GetAsync(FilterDto filter);
        Task<ProductDto> UpdateAsync(ProductDto productDto);
    }
}
