using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using OEDClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WordOrganizerService;

namespace Api.Controllers
{
    [ApiController]
    [Route("{instanceId:guid}")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWordOrganizerService wordOrganizer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IWordOrganizerService wordOrganizer)
        {
            _logger = logger;
            this.wordOrganizer = wordOrganizer;
        }

        [HttpGet]
        [Route("define/{word}")]
        public async Task<IEnumerable<WordInformation>> GetAndSaveDefinitionAsync(Guid instanceId, string word)
        {
            return await this.wordOrganizer.GetAndSaveWordInformation(instanceId, word);
        }

        [HttpGet]
        [Route("words")]
        public IEnumerable<string> GetAllWords(Guid instanceId)
        {
            return this.wordOrganizer.GetAllWordsForInstance(instanceId);
        }
    }
}
