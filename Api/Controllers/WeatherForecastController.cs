using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using OEDClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOxfordDictionaryClient client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IOxfordDictionaryClient client)
        {
            _logger = logger;
            this.client = client;
        }

        [HttpGet]
        public async Task<IEnumerable<WordInformation>> Get()
        {
            return await this.client.GetInformation("test");
        }
    }
}
