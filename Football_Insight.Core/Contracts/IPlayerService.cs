using Football_Insight.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int id);
        Task CreatePlayerAsync(Player player);
        Task UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(int id);
    }
}
