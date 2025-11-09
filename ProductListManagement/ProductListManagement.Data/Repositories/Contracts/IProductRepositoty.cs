using ProductListManagement.Data.Contracts;
using ProductListManagement.Model;
using System.Linq.Expressions;

namespace ProductListManagement.Data.Repositories.Contracts
{
    public interface IProductRepositoty : IRepository
    {
        Task AddAssync(Product entity);
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> expression = null);
    }
}
