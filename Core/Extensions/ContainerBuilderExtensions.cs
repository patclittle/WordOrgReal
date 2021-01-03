using System;
using Autofac;
using Autofac.Builder;
using Autofac.Features.AttributeFilters;

namespace Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterSingleInstance<TImplementer>(this ContainerBuilder builder) where TImplementer : notnull
        {
            return builder.RegisterType<TImplementer>().AsImplementedInterfaces().WithAttributeFiltering().SingleInstance();
        }
    }
}
