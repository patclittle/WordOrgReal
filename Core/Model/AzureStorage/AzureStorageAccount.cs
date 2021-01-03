using Microsoft.Azure.Cosmos.Table;

namespace Core.Model.AzureStorage
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
