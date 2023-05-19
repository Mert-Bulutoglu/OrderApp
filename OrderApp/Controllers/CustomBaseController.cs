using Microsoft.AspNetCore.Mvc;
using OrderApp.API.Filter;

namespace OrderApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateFilter]
    public class CustomBaseController : ControllerBase
    {

    }
}
