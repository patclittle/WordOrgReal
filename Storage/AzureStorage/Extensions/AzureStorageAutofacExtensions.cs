using System;
using Autofac;
using Autofac.Builder;
using Storage.AzureStorage.Model;
using Microsoft.Azure.Cosmos.Table;

namespace Storage.AzureStorage.Extensions
{
    public static class AzureStorageAutofacExtensions
    {

        public static IRegistrationBuilder<CloudTable, SimpleActivatorData, SingleRegistrationStyle> RegisterNamedCloudTable(
            this ContainerBuilder builder,
            AzureStorageAccount account,
            string tableName)
        {
            return builder.Register(c => GetCloudTable(c, account, tableName))
                .Named<CloudTable>(tableName)
                .SingleInstance();
        }

        private static CloudTable GetCloudTable(IComponentContext context, AzureStorageAccount account, string tableName)
        {
            var storageService = context.Resolve<IAzureStorageAccountService>();
            var storageAccount = storageService.GetStorageAccount(account);
            var table = storageAccount.GetStorageTable(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}
