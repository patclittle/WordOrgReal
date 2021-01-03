using Core.Model.AzureStorage;

namespace Storage.AzureStorage
{
    internal interface IAzureStorageAccountService
    {
        ICloudStorageAccount GetStorageAccount(AzureStorageAccount storageAccount);
    }
}