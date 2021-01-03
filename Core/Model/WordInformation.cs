using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model.OEDResponse;

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

        public string WordName { get; }

        public string Definition { get; }

        public PartOfSpeech PartOfSpeech { get; }

        public IEnumerable<string> Domains { get; }

        public IEnumerable<string> Registers { get; }

        public IEnumerable<string> SemanticClasses { get; }

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
                default:
                    throw new NotImplementedException("Not implemented for " + posString);
            }
        }
    }
}
