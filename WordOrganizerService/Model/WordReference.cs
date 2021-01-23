using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WordOrganizerService.Model
{
    public class WordReference
    {
        public WordReference(
            string wordName,
            IEnumerable<WordInformation> wordInfo)
        {
            this.WordName = wordName;
            this.PartsOfSpeech = wordInfo.Select(w => w.PartOfSpeech);
        }

        public WordReference()
        {
        }

        public string WordName { get; set; }

        [IgnoreProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public IEnumerable<PartOfSpeech> PartsOfSpeech { get; set; }

        public string PartsOfSpeechSerialized { get; set; }

        public void Serialize()
        {
            this.PartsOfSpeechSerialized = this.PartsOfSpeech == null ? "[]" : JsonConvert.SerializeObject(this.PartsOfSpeech);
        }

        public void Deserialize()
        {
            this.PartsOfSpeech = JsonConvert.DeserializeObject<IEnumerable<PartOfSpeech>>(this.PartsOfSpeechSerialized);
        }
    }
}
