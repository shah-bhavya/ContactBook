using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ContactBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllowOrigin")]
    public class BaseController : ControllerBase
    {
       
    }
}
