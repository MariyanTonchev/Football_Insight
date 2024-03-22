using Football_Insight.Core.Contracts;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IRepository repo;

        public StadiumService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> GetStadiumIdAsync(int teamId)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);

            return team.StadiumId;
        }
    }
}
