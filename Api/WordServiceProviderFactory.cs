namespace Api
{
    using System;
    using Autofac;
    using Autofac.Builder;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public class WordServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        public WordServiceProviderFactory()
        {
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null)
            {
                throw new ArgumentNullException(nameof(containerBuilder));
            }

            return new AutofacServiceProvider(containerBuilder.Build(ContainerBuildOptions.None));
        }
    }
}