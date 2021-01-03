using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDWordResult
    {
        [JsonConstructor]
        public OEDWordResult(
            string id,
            string word,
            IEnumerable<OEDLexicalEntry> lexicalEntries)
        {
            this.Id = id;
            this.Word = word;
            this.LexicalEntries = lexicalEntries;
        }

        public string Id { get; }
        public string Word { get; }
        public IEnumerable<OEDLexicalEntry> LexicalEntries { get; }
    }
}
