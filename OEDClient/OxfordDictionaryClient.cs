using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.OEDResponse;
using Newtonsoft.Json;

namespace OEDClient
{
    public class OxfordDictionaryClient : IOxfordDictionaryClient
    {
        private readonly string baseAddress = "https://od-api.oxforddictionaries.com/api/v2/entries/en-gb/";
        private readonly HttpClient httpClient;

        public OxfordDictionaryClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<WordInformation>> GetInformation(string word)
        {
            using (var req = new HttpRequestMessage(HttpMethod.Get, baseAddress + word))
            {
                req.Headers.Add("app_id", "757a4cb7");
                req.Headers.Add("app_key", "391f43296b91d426a938d22eee70050d");
                req.Headers.Add("Accept", "application/json");
                var res = await this.httpClient.SendAsync(req);
                var content = await res.Content.ReadAsStringAsync();
                return WordInformation.FromOedResponse(JsonConvert.DeserializeObject<OEDWordResponse>(content));
            }
        }
    }
}
