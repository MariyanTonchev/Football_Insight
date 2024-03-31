using Football_Insight.Core.Contracts;
using Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Core.Services
{
    public class PlayerService : IPlayerService
    {
        public Task CreatePlayerAsync(Player player)
        {
            throw new ArgumentNullException(nameof(player));
        }

        public Task DeletePlayerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
