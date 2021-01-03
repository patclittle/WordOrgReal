using System;
using Microsoft.Azure.Cosmos.Table;

namespace Core.Model.AzureStorage
{
    public interface ICloudStorageAccount
    {
        CloudTable GetStorageTable(string tableName);
    }
}
