using Autofac;
using ProductListManagement.Service.Contracts;
using ProductListManagement.Services;
using ProductListManagement.Services.Contracts;
using ProductListManagement.Services.Mappers;

namespace ProductListManagement.Service
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes(ContainerBuilder builder, ProductListManagementSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            builder.RegisterInstance(settings);
            MapperFactory.Configure(builder);

            //register service
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<FilterService>().As<IFilterService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<ValidationService>().As<IValidationService>();

            Data.ContainerConfiguration.RegisterTypes(builder, settings.ConnectionStrings);
        }
    }
}
