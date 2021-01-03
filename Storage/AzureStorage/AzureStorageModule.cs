using System;
using Autofac;
using Core.Extensions;

namespace Storage.AzureStorage
{
    public class AzureStorageModule : Module
    {
        public AzureStorageModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSingleInstance<AzureStorageAccountService>();
            base.Load(builder);
        }
    }
}
