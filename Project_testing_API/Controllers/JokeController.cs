
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class JokeController : Controller
    {
        private ILogger<JokeController> _logger;


        public JokeController(ILogger<JokeController> logger)
        {
            _logger = logger;
        }


        //GETS ALL ENDPOINTS
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var url = "https://v2.jokeapi.dev/endpoints";
            string stringResponse;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resonseContent = response.Content;
                        stringResponse = await resonseContent.ReadAsStringAsync();
                        var serializedResponse = JsonConvert.SerializeObject(stringResponse);
                        return Ok(stringResponse);
                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                }
            }




        }


        // GETS A RANDOM JOKE
        [HttpGet("{category}")]
        public async Task<IActionResult> Get(string category)
        {

            var endpoint = $"https://v2.jokeapi.dev/joke/{category}?blacklistFlags=nsfw,racist,sexist,explicit&type=single&amount=1";
            string stringResponse;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resonseContent = response.Content;
                        stringResponse = await resonseContent.ReadAsStringAsync();
                        var serializedResponse = JsonConvert.SerializeObject(stringResponse);
                        return Ok(stringResponse);
                    }
                    else
                    {
                        return BadRequest("Error");
                    }
                }
            }


        }

        //GETS A NUMBER OF RANDOM JOKES(MAX 10)
        [HttpGet("{number:int}")]
        public async Task<IActionResult> Get(int number)
        {
            if (number > 10)
            {
                number = 10;
            }
            var endpoint = $"https://v2.jokeapi.dev/joke/Any??blacklistFlags=nsfw,racist,sexist,explicit&type=single&type=single&amount={number}";

            string stringResponse;
            using (var client = new HttpClient())
            {

                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resonseContent = response.Content;
                        stringResponse = await resonseContent.ReadAsStringAsync();
                        var serializedResponse = JsonConvert.SerializeObject(stringResponse);
                        return Ok(stringResponse);
                    }
                    else
                    {
                        return BadRequest("Error");
                    }

                }

            }


        }



    }
}