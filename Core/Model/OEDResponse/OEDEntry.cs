using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDEntry
    {
        [JsonConstructor]
        public OEDEntry(IEnumerable<OEDSense> senses)
        {
            this.Senses = senses;
        }

        public IEnumerable<OEDSense> Senses { get; }
    }
}