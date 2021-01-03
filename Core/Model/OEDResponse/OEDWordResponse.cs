using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDWordResponse
    {
        [JsonConstructor]
        public OEDWordResponse(
            string id,
            IEnumerable<OEDWordResult> results,
            string word)
        {
            this.Id = id;
            this.Results = results;
            this.Word = word;
        }

        public string Id { get; }

        public IEnumerable<OEDWordResult> Results { get; }

        public string Word { get; }
    }
}
