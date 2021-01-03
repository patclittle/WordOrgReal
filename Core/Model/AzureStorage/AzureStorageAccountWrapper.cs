using System;
using Microsoft.Azure.Cosmos.Table;

namespace Core.Model.AzureStorage
{
    public class AzureStorageAccountWrapper : ICloudStorageAccount
    {
        private readonly CloudTableClient tableClient;

        public AzureStorageAccountWrapper(CloudStorageAccount tableAccount)
        {
            this.tableClient = tableAccount.CreateCloudTableClient();
        }

        public CloudTable GetStorageTable(string tableName)
        {
            return this.tableClient.GetTableReference(tableName);
        }
    }
}
