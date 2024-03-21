using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IRepository repo;

        public LeagueService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<List<LeagueTeamsViewModel>> GetAllLeaguesWithTeamsAsync()
        {
            var leagues = await repo.All<League>()
                        .Select(l => new LeagueTeamsViewModel
                        {
                            Id = l.Id,
                            Name = l.Name,
                            Teams = repo.All<Team>()
                                .Where(t => t.LeagueId == l.Id)
                                .Select(t => new TeamSimpleViewModel
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                })
                                .ToList()
                        })
                        .ToListAsync();

            return leagues;
        }
    }
}
