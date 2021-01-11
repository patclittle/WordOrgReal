﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model.OEDResponse;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace Core.Model
{
    public class WordInformation
    {
        public WordInformation(
            string wordName,
            string definition,
            PartOfSpeech partOfSpeech,
            IEnumerable<string> domains,
            IEnumerable<string> registers,
            IEnumerable<string> semanticClasses)
        {
            this.WordName = wordName;
            this.Definition = definition;
            this.PartOfSpeech = partOfSpeech;
            this.Domains = domains;
            this.Registers = registers;
            this.SemanticClasses = semanticClasses;
        }

        public WordInformation()
        {
        }

        public string WordName { get; set; }

        public string Definition { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }

        [IgnoreProperty]
        public IEnumerable<string> Domains { get; set; }

        public string DomainsSerialized { get; set; }

        [IgnoreProperty]
        public IEnumerable<string> Registers { get; set; }

        public string RegistersSerialized { get; set; }

        [IgnoreProperty]
        public IEnumerable<string> SemanticClasses { get; set; }

        public string SemanticClassesSerialized { get; set; }

        public void Serialize()
        {
            this.DomainsSerialized = this.Domains == null ? "[]" : JsonConvert.SerializeObject(this.Domains);
            this.RegistersSerialized = this.Registers == null ? "[]" : JsonConvert.SerializeObject(this.Registers);
            this.SemanticClassesSerialized = this.SemanticClasses == null ? "[]" : JsonConvert.SerializeObject(this.SemanticClasses);
        }

        public void Deserialize()
        {
            this.Domains = JsonConvert.DeserializeObject<IEnumerable<string>>(this.DomainsSerialized);
            this.Registers = JsonConvert.DeserializeObject<IEnumerable<string>>(this.RegistersSerialized);
            this.SemanticClasses = JsonConvert.DeserializeObject<IEnumerable<string>>(this.SemanticClassesSerialized);
        }

        public static IEnumerable<WordInformation> FromOedResponse(OEDWordResponse oedResponse)
        {
            var wordName = oedResponse.Word;
            foreach (var lexicalEntry in oedResponse.Results.SelectMany(r => r.LexicalEntries))
            {
                var partOfSpeech = GetPartOfSpeech(lexicalEntry.LexicalCategory.Id);
                foreach (var sense in lexicalEntry.Entries.SelectMany(e => e.Senses))
                {
                    if (sense.Definitions != null && sense.Definitions.Any())
                    {
                        yield return new WordInformation(
                        wordName,
                        sense.Definitions.First(),
                        partOfSpeech,
                        sense.DomainClasses?.Select(d => d.Id) ?? new List<string>(),
                        sense.Registers?.Select(d => d.Id) ?? new List<string>(),
                        sense.SemanticClasses?.Select(d => d.Id) ?? new List<string>());
                    }
                }
            }
        }

        private static PartOfSpeech GetPartOfSpeech(string posString)
        {
            switch (posString.ToLower())
            {
                case "noun":
                    return PartOfSpeech.Noun;
                case "adjective":
                    return PartOfSpeech.Adjective;
                case "verb":
                    return PartOfSpeech.Verb;
                case "adverb":
                    return PartOfSpeech.Adverb;
                case "article":
                    return PartOfSpeech.Article;
                case "pronoun":
                    return PartOfSpeech.Pronoun;
                case "interjection":
                    return PartOfSpeech.Interjection;
                default:
                    throw new NotImplementedException("Not implemented for " + posString);
            }
        }
    }
}
