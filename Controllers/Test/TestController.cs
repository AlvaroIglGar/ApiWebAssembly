using ApiRestDespliegue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiRestDespliegue.Controllers.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MongoDbService _mongoService;

        public TestController(MongoDbService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _mongoService.Users.Find(_ => true).ToList();
            return Ok(users);
        }
    }
}
