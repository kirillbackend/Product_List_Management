using Autofac;

namespace ProductListManagement
{
    public class ContainerConfiguration
    {
        public static void ResisterTypes(ContainerBuilder builder, ApiSettings settings)
        {
            builder.RegisterInstance(settings);

            builder.RegisterInstance(new LoggerFactory())
                .As<ILoggerFactory>();

            builder.RegisterGeneric(typeof(Logger<>))
                   .As(typeof(ILogger<>))
                   .SingleInstance();

            Service.ContainerConfiguration.RegisterTypes(builder,settings);
        }
    }
}
