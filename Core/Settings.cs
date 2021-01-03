using System;
using Core.Model.AzureStorage;

namespace Core
{
    public static class Settings
    {
        public static class Storage
        {
            public const string MainTableName = "mainstorage";
            public static AzureStorageAccount MainStorageAccount { get; } = new AzureStorageAccount("DefaultEndpointsProtocol=https;AccountName=wordorganizermainstorage;AccountKey=n1b3Qb5vuNn4lwyB25Wbw3aM/JKipfqgFDMKx6UqVnHYFJyKjed8riMpvXZC7aAgQzCgB83cwEeRemmAx2vW8w==;EndpointSuffix=core.windows.net");
        }
    }
}
