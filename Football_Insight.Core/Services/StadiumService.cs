using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Stadium;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IRepository repo;

        public StadiumService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<List<StadiumSimpleViewModel>> GetAllStadiumAsync()
        {
            return await repo.AllReadonly<Stadium>()
                .Select(s => new StadiumSimpleViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToListAsync();
        }

        public async Task<int> GetStadiumIdAsync(int teamId)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);

            return team.StadiumId;
        }
    }
}
