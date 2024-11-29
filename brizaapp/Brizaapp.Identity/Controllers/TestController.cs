using Microsoft.AspNetCore.Mvc;

namespace Brizaapp.Identity.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TestController : ControllerBase
  {
    [HttpGet(Name = "GetTest")]
    public IEnumerable<string> Get()
    {
      
      return new List<string>
            {
                "First result",
                "Second result",
                "Third result"
            };
    }
  }
}

