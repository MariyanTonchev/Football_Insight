using Football_Insight.Data;
using Football_Insight.Models.Coach;
using Football_Insight.Models.League;
using Football_Insight.Models.Stadium;
using Football_Insight.Models.Team;
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

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var team = await context.Teams
                .Where(t => t.Id == Id)
                .Include(t => t.Coach)
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync();

            var viewModel = new TeamDetailedViewModel
            {
                Id = Id,
                Name = team.Name,
                Founded = team.Founded,
                LogoURL = team.LogoURL,
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
                Coach = new CoachSimpleViewModel
                {
                    Id = team.Coach.Id,
                    Name = $"{team.Coach.FirstName} {team.Coach.LastName}"
                },
            };

            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
