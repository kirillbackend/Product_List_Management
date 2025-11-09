using Autofac;
using ProductListManagement.Data.Contracts;
using ProductListManagement.Data.Repositories;
using ProductListManagement.Data.Repositories.Contracts;

namespace ProductListManagement.Data
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes(ContainerBuilder builder, DbConnectionSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            builder.RegisterInstance(settings);

            builder.RegisterType<DataContextManager>().As<IDataContextManager>();
            builder.RegisterType<ProductRepositoty>().As<IProductRepositoty>();
        }
    }
}
