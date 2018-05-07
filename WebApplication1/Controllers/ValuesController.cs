using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var random = new Random();
            var value = random.Next(1, 30);
            if (value < 15)
            {
                throw new NullReferenceException();
            }
            else if (value < 20)
            {
                throw new InvalidOperationException();
            }
            else if (value < 24)
            {
                throw new ArgumentNullException();
            }
            else if (value < 27)
            {
                throw new ArgumentException();
            }
            else if (value < 29)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                throw new DivideByZeroException();
            }
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation($"Object with id {id} retrieved");
            return "value";
        }

        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}