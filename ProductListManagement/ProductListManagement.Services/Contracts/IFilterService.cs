using ProductListManagement.Model;
using ProductListManagement.Service.Dtos;
using System.Linq.Expressions;

namespace ProductListManagement.Service.Contracts
{
    public interface IFilterService
    {
        Task<Expression<Func<Product, bool>>> CreateProductFilter(FilterDto filter);
    }
}
