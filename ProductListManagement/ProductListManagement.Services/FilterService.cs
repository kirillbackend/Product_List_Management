using ProductListManagement.Service.Contracts;
using System.Linq.Expressions;
using ProductListManagement.Model;
using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Service
{
    public class FilterService : IFilterService
    {
        public async Task<Expression<Func<Product, bool>>> CreateProductFilter(FilterDto filter)
        {
            var predicate = PredicateBuilder.True<Product>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                predicate = predicate.And(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.Price.HasValue)
            {
                predicate = predicate.And(p => p.Price == filter.Price);
            }

            if (filter.MinPrice.HasValue)
            {
                predicate = predicate.And(p => p.Price >= filter.MinPrice);
            }

            if (filter.MaxPrice.HasValue) 
            {
                predicate = predicate.And(p => p.Price <= filter.MaxPrice);
            }

            return predicate;
        }
    }
}
