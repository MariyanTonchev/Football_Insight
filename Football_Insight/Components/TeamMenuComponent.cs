using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Components
{
    public class TeamMenuComponent : ViewComponent
    {
        private readonly FootballInsightDbContext context;

        public TeamMenuComponent(FootballInsightDbContext Context)
        {
            context = Context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var team = await context.Teams
                .Where(t => t.Id == teamId)
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync();

            var viewModel = new TeamDetailedViewModel
            {
                TeamId = teamId,
                Name = team.Name,
                Founded = team.Founded,
                LogoURL = team.LogoURL,
                Coach = team.Coach,
                League = new LeagueSimpleViewModel
                {
                    Id = team.League.Id,
                    Name = team.League.Name,
                },
                Stadium = new StadiumSimpleViewModel
                {
                    Id = team.Stadium.Id,
                    Name = team.Stadium.Name
                },
            };

            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
