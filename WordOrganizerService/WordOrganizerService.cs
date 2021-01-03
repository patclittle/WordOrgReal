using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Core.Model;
using Microsoft.Azure.Cosmos.Table;
using OEDClient;

namespace WordOrganizerService
{
    public class WordOrganizerService
    {
        private readonly CloudTable mainTable;
        private readonly IOxfordDictionaryClient oedClient;

        public WordOrganizerService(
            [KeyFilter("MainTable")] CloudTable mainTable,
            IOxfordDictionaryClient oedClient)
        {
            this.mainTable = mainTable;
            this.oedClient = oedClient;
        }

        public async Task<IEnumerable<WordInformation>> GetAndSaveWordInformation(Guid instanceId, string word)
        {
            var wordInfo = await oedClient.GetInformation(word);
            foreach (var info in wordInfo)
            {
                var tableEntity = new TableEntityAdapter<WordInformation>()
                {
                    PartitionKey = instanceId.ToString(),
                    RowKey = word,
                    OriginalEntity = info,
                };

                await mainTable.ExecuteAsync(TableOperation.Insert(tableEntity));
            }

            return wordInfo;
        }
    }
}
