using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project_testing_API.Controllers
{
 
    [EnableCors("CORSPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class Jokev2Controller : Controller
    {

        private readonly ILogger<Jokev2Controller> _logger;
        private readonly IMemoryCache _memoryCache;

        public Jokev2Controller(ILogger<Jokev2Controller> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }



        //GET A RANDOM JOKE
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://joke3.p.rapidapi.com/v1/joke?nsfw=false"),
                Headers =
                {
                    { "x-rapidapi-key", "9b86471903mshfd57534b68f6760p1f7347jsnee6d92e25578"
                    },
                    { "x-rapidapi-host", "joke3.p.rapidapi.com"
                    },
                }
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
                else
                {
                    return BadRequest("Error");
                }

            }

        }

        // GET A JOKE WITH ID 


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {

                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://joke3.p.rapidapi.com/v1/joke/{id}"),
                Headers =
                {
                    { "x-rapidapi-key", "9b86471903mshfd57534b68f6760p1f7347jsnee6d92e25578"
                    },
                    { "x-rapidapi-host", "joke3.p.rapidapi.com"
                    },
                }
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
                else
                {
                    return BadRequest("Error");
                }
            }

        }

        //UPVOTE A JOKE WITH ID
        [HttpPost]
        public async Task<IActionResult> Post(string Id)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://joke3.p.rapidapi.com/v1/joke/{Id}/upvote"),
                Headers =
                {
                    { "x-rapidapi-key", "9b86471903mshfd57534b68f6760p1f7347jsnee6d92e25578"
                    },
                    { "x-rapidapi-host", "joke3.p.rapidapi.com"
                    },
                }

            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
                else
                {
                    return BadRequest("Error");
                }

            }
        }

        //DOWNVOTE A JOKE 
        [HttpPost]
        [Route("action")]
        public async Task<IActionResult> PostDownVote(string Id)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://joke3.p.rapidapi.com/v1/joke/{Id}/downvote"),
                Headers =
                {
                    { "x-rapidapi-key", "9b86471903mshfd57534b68f6760p1f7347jsnee6d92e25578"
                    },
                    { "x-rapidapi-host", "joke3.p.rapidapi.com"
                    },
                }

            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
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

