using Microsoft.AspNetCore.Mvc;
using Recipe.Core.Exceptions;

namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("client-error")]
        public IActionResult GetClientError()
        {
            throw new ClientSideException();
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFoundError()
        {
            throw new NotFoundException("Resource not found.");
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server-side exception.");
        }
    }
}