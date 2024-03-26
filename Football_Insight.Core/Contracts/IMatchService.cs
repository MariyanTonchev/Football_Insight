using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;

namespace Football_Insight.Core.Contracts
{
    public interface IMatchService
    {
        Task<ActionResult> CreateMatchAsync(MatchCreateViewModel model);
    }
}
