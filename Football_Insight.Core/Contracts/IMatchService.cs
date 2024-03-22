using Football_Insight.Core.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Contracts
{
    public interface IMatchService
    {
        Task<MatchCreateResultViewModel> CreateMatchAsync(MatchCreateViewModel model);
    }
}
