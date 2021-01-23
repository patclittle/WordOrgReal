using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Core;
using Core.Model;
using Microsoft.Azure.Cosmos.Table;
using OEDClient;
using WordOrganizerService.Model;

namespace WordOrganizerService
{
    public class WordOrganizerService : IWordOrganizerService
    {
        private readonly CloudTable definitionsTable;
        private readonly CloudTable wordReferencesTable;
        private readonly IOxfordDictionaryClient oedClient;

        public WordOrganizerService(
            [KeyFilter(Settings.Storage.Definitions)] CloudTable definitionsTable,
            [KeyFilter(Settings.Storage.WordReferences)] CloudTable wordReferencesTable,
            IOxfordDictionaryClient oedClient)
        {
            this.definitionsTable = definitionsTable;
            this.wordReferencesTable = wordReferencesTable;
            this.oedClient = oedClient;
        }

        public IEnumerable<WordReference> GetAllWordsForInstance(Guid instanceId)
        {
            return this.GetAllWordsFromReferenceTable(instanceId);
        }

        public async Task<IEnumerable<WordInformation>> GetAndSaveWordInformation(Guid instanceId, string word)
        {
            var existingDefs = this.GetExistingDefinitionsForWord(instanceId, word);
            if (existingDefs.Any())
            {
                return existingDefs;
            }
            else
            {
                var wordInfo = await oedClient.GetInformation(word);
                await this.AddWordToReferenceTableAsync(instanceId, word, wordInfo);
                foreach (var info in wordInfo)
                {
                    await this.AddDefinitionToTableAsync(instanceId, info);
                }

                return wordInfo;
            }
            
        }

        private async Task AddDefinitionToTableAsync(Guid instanceId, WordInformation word)
        {
            word.Serialize();
            var tableEntity = new TableEntityAdapter<WordInformation>()
            {
                PartitionKey = $"{instanceId}_{word.WordName}",
                RowKey = Guid.NewGuid().ToString(),
                OriginalEntity = word,
            };

            await definitionsTable.ExecuteAsync(TableOperation.Insert(tableEntity));
        }

        private async Task AddWordToReferenceTableAsync(Guid instanceId, string wordName, IEnumerable<WordInformation> wordInfo)
        {
            var wordRef = new WordReference(wordName, wordInfo);
            wordRef.Serialize();
            var tableEntity = new TableEntityAdapter<WordReference>()
            {
                PartitionKey = instanceId.ToString(),
                RowKey = wordName,
                OriginalEntity = wordRef,
            };

            await this.wordReferencesTable.ExecuteAsync(TableOperation.Insert(tableEntity));
        }

        private IEnumerable<WordInformation> GetExistingDefinitionsForWord(Guid instanceId, string word)
        {
            var query = new TableQuery<TableEntityAdapter<WordInformation>>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", "eq", $"{instanceId}_{word}"));
            var res = this.definitionsTable.ExecuteQuery(query);
            var infos = res.Select(r => r.OriginalEntity).ToList();
            foreach (var info in infos)
            {
                info.Deserialize();
            }
            return infos;
        }

        private IEnumerable<WordReference> GetAllWordsFromReferenceTable(Guid instanceId)
        {
            var query = new TableQuery<TableEntityAdapter<WordReference>>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", "eq", $"{instanceId}"));
            var res = this.wordReferencesTable.ExecuteQuery(query);
            return res.Select(r => r.OriginalEntity);
        }
    }
}
