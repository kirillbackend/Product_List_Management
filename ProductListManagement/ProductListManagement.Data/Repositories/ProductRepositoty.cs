using Microsoft.EntityFrameworkCore;
using ProductListManagement.Data.Repositories.Contracts;
using ProductListManagement.Model;
using System.Linq.Expressions;

namespace ProductListManagement.Data.Repositories
{
    public class ProductRepositoty : AbstractRepository<Product>, IProductRepositoty
    {
        public ProductRepositoty(ProductListManagementDataContext context) : base(context)
        {
        }

        public async Task AddAssync(Product entity)
        {
            Context.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            IQueryable<Product> query = Context.Products;

            var category = await query.FirstOrDefaultAsync(i => i.Id == id);

            return category;
        }

        public async Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> expression = null)
        {
            IQueryable<Product> query = Context.Products;
            var finances = await query.Where(expression).ToListAsync();

            return finances;
        }
    }
}
