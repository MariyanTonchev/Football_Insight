using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Components
{
    public class TeamMenuComponent : ViewComponent
    {
        private readonly IRepository repository;

        public TeamMenuComponent(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var team = await repository.AllReadonly<Team>(t => t.Id == teamId)
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
