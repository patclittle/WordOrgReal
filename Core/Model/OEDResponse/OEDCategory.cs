using Newtonsoft.Json;

namespace Core.Model.OEDResponse
{
    public class OEDCategory
    {
        [JsonConstructor]
        public OEDCategory(
            string id)
        {
            this.Id = id;
        }

        public string Id { get; }
    }
}