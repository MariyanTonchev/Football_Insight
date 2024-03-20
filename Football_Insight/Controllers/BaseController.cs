using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
