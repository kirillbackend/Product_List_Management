using Autofac;
using ProductListManagement.Data.Contracts;

namespace ProductListManagement.Data
{
    internal class DataContextManager : IDataContextManager
    {
        private readonly object _contextLock = new object();
        private Dictionary<string, ProductListManagementDataContext> _contexts = new Dictionary<string, ProductListManagementDataContext>();
        private DbConnectionSettings _connectionSettings;
        private readonly ILifetimeScope _container;

        public DataContextManager(ILifetimeScope container, DbConnectionSettings connectionSettings)
        {
            _container = container;
            _connectionSettings = connectionSettings;
        }

        public T CreateRepository<T>(string id = "default")
           where T : class, IRepository
        {
            return _container.Resolve<T>(new TypedParameter(typeof(ProductListManagementDataContext), GetDataContext(id)));
        }

        public async Task SaveAsync(string id = "default")
        {
            var contextKey = id;

            await _contexts[contextKey].SaveChangesAsync();
        }

        #region private metods

        private ProductListManagementDataContext GetDataContext(string id = "default")
        {
            var contextKey = id;

            lock (_contextLock)
            {

                if (!_contexts.ContainsKey(contextKey))
                {
                    _contexts[contextKey] = new ProductListManagementDataContext(_connectionSettings);
                }

                return _contexts[contextKey];
            }
        }

        #endregion
    }
}
