using Core.Model.AzureStorage;

namespace Storage.AzureStorage
{
    internal class AzureStorageAccountService : IAzureStorageAccountService
    {
        public AzureStorageAccountService()
        {
        }

        public ICloudStorageAccount GetStorageAccount(AzureStorageAccount storageAccount)
        {
            var tableStorageAccount = storageAccount.GetTableStorageAccount();
            return new AzureStorageAccountWrapper(tableStorageAccount);
        }
    }
}
