using Microsoft.Extensions.Logging;
using ProductListManagement.Data.Contracts;
using ProductListManagement.Service.Mappers.Contracts;

namespace ProductListManagement.Services
{
    public abstract class AbstractService
    {
        public ILogger Logger { get; }
        public IMapperFactory MapperFactory { get; }
        public IDataContextManager DataContextManager { get; }

        public AbstractService(ILogger logger, IMapperFactory mapperFactory, IDataContextManager dataContextManager)
        {
            Logger = logger;
            MapperFactory = mapperFactory;
            DataContextManager = dataContextManager;
        }
    }
}
