using Football_Insight.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly FootballInsightDbContext data;

        public BaseController(FootballInsightDbContext context)
        {
            data = context;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ViewData["Leagues"] = await data.Leagues
                .Select(l => new { l.Id, l.Name })
                .ToListAsync();

            await next();
        }
    }
}
