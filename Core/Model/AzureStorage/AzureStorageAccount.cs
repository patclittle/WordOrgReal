using Microsoft.Azure.Cosmos.Table;

namespace Storage.AzureStorage.Model
{
    public class AzureStorageAccount
    {
        private readonly string connectionString;

        public AzureStorageAccount(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public CloudStorageAccount GetTableStorageAccount()
        {
            return CloudStorageAccount.Parse(this.connectionString);
        }
    }
}
