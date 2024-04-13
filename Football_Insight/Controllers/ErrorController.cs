using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/InternalServerError")]
        public IActionResult InternalServerError()
        {
            return View();
        }

        [Route("Error/HandleError")]
        public IActionResult HandleError(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            } 
            
            if(statusCode == 401)
            {
                return View("AccessDenied");
            }

            return View(nameof(InternalServerError));
        }
    }
}
