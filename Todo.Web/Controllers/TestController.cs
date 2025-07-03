using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Todo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string GetSomething()
        {
            return "Pasword";
        }
    }
}
