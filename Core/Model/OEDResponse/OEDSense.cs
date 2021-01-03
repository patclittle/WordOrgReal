using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDSense
    {
        [JsonConstructor]
        public OEDSense(
            IEnumerable<string> definitions,
            IEnumerable<OEDCategory> domainClasses,
            IEnumerable<OEDCategory> semanticClasses,
            IEnumerable<OEDCategory> registers)
        {
            this.Definitions = definitions;
            this.DomainClasses = domainClasses;
            this.SemanticClasses = semanticClasses;
            this.Registers = registers;
        }

        public IEnumerable<string> Definitions { get; }

        public IEnumerable<OEDCategory> DomainClasses { get; }

        public IEnumerable<OEDCategory> SemanticClasses { get; }

        public IEnumerable<OEDCategory> Registers { get; }
    }
}