using Football_Insight.Data;
using Football_Insight.Models.League;
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
    }
}
