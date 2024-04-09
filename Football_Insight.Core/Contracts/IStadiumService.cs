using Football_Insight.Core.Models.Stadium;

namespace Football_Insight.Core.Contracts
{
    public interface IStadiumService
    {
        Task<int> GetStadiumIdAsync(int teamId);

        Task<List<StadiumSimpleViewModel>> GetAllStadiumAsync();
    }
}
