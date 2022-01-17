using HonsBackendAPI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [APIKey]
    public class AdminsController : ControllerBase
    {
    }
}
