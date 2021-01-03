using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDLexicalEntry
    {
        [JsonConstructor]
        public OEDLexicalEntry(
            OEDCategory lexicalCategory,
            IEnumerable<OEDEntry> entries)
        {
            this.LexicalCategory = lexicalCategory;
            this.Entries = entries;
        }

        public OEDCategory LexicalCategory { get; }

        public IEnumerable<OEDEntry> Entries { get; }
    }
}