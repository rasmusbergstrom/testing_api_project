using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeApi.Controllers
{
    [EnableCors("CORSPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FootballController : ControllerBase
    {
            

        private readonly ILogger<FootballController> _logger;
        private readonly IMemoryCache _memoryCache;

        public FootballController(ILogger<FootballController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }



        //GET A FOTBALLER BY ID

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cacheKey = $"Get_Player-{id}";


            if (_memoryCache.TryGetValue(cacheKey, out string cachedValue))
                return Ok(cachedValue);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.football-data.org/v2/players/{id}"),
                Headers =
                {
                    { "X-Auth-Token", "d7620d83f20d4a28be7b7d19d835fae6"
                    },

                }
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    _memoryCache.Set(cacheKey, body);
                    return Ok(body);
                }
                else
                {
                    return BadRequest("Error");
                }
            }

        }
    }
}
