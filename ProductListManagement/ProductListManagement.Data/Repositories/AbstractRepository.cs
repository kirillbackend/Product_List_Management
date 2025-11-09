

namespace ProductListManagement.Data.Repositories
{
    public abstract class AbstractRepository<T>
    {
        protected readonly ProductListManagementDataContext Context;

        public AbstractRepository(ProductListManagementDataContext context)
        {
            Context = context;
        }
    }
}
