using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Player;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Football_Insight.Core.Constants.GlobalConstants;

namespace Football_Insight.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository repository;

        public PlayerService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<OperationResult> CreatePlayerAsync(PlayerFormViewModel viewModel)
        {
            if(viewModel.SelectedPosition == -1)
            {
                return new OperationResult(false, "Position is required.");
            }

            var player = new Player
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DateOfBirth = viewModel.DateOfBirth,
                Position = viewModel.SelectedPosition,
                Price = viewModel.Price,
                Salary = viewModel.Salary,
                TeamId = viewModel.TeamId
            };

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();

            return new OperationResult(true, "Successfully created player.");
        }

        public PlayerFormViewModel GetCreateFomViewModel(int teamId)
        {
            var list = new PlayerFormViewModel
            {
                Positions = GetPositionsFromEnum(),
                TeamId = teamId
            };

            return list;
        }

        public async Task<PlayerFormViewModel> GetEditFormViewModel(int playerId)
        {
            var player = await repository.GetByIdAsync<Player>(playerId);

            var viewModel = new PlayerFormViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                SelectedPosition = player.Position,
                Salary = player.Salary,
                TeamId = player.TeamId,
                Positions = GetPositionsFromEnum(),
                Price = player.Price,
            };

            return viewModel;
        }

        public async Task<OperationResult> UpdatePlayerAsync(PlayerFormViewModel viewModel, int playerId)
        {
            var player = await GetPlayerAsync(playerId);

            if (player != null)
            {
                player.FirstName = viewModel.FirstName;
                player.LastName = viewModel.LastName;
                player.DateOfBirth = viewModel.DateOfBirth;
                player.Salary = viewModel.Salary;
                player.TeamId = viewModel.TeamId;
                player.Position = viewModel.SelectedPosition;
                player.TeamId = viewModel.TeamId;
                player.Price = viewModel.Price;

                await repository.SaveChangesAsync();

                return new OperationResult(true, "Player edited successfully!");
            }
            else
            {
                return new OperationResult(false, "Player not found!");
            }
        }

        public ICollection<SelectListItem> GetPositionsFromEnum()
        {
            var positions = Enum.GetValues(typeof(PlayerPosition))
                    .Cast<PlayerPosition>()
                    .Select(p => new SelectListItem
                    {
                        Value = ((int)p).ToString(),
                        Text = p.ToString()
                    })
                    .ToList();

            return positions;
        }

        private async Task<Player> GetPlayerAsync(int playerId) => await repository.GetByIdAsync<Player>(playerId);

        public async Task<PlayerSimpleViewModel> GetPlayerSimpleViewModelAsync(int playerId)
        {
            var player = await GetPlayerAsync(playerId);

            var viewModel = new PlayerSimpleViewModel
            {
                Name = $"{player.FirstName} {player.LastName}",
                PlayerId = playerId,
                TeamId = player.TeamId
            };

            return viewModel;
        }

        public async Task<OperationResult> DeletePlayerAsync(int playerId)
        {
            var player = await GetPlayerAsync(playerId);

            if (player == null)
            {
                return new OperationResult(false, "Player not found.");
            }

            if (await HasGoals(playerId))
            {
                return new OperationResult(false, "You cannot delete a player with goals.");
            }

            if (await HasAssists(playerId))
            {
                return new OperationResult(false, "You cannot delete a player with assists.");
            }

            await repository.RemoveAsync(player);
            await repository.SaveChangesAsync();

            return new OperationResult(true, $"Successfully deleted player with ID:{player.Id} and Name: {player.FirstName} {player.LastName}!");
        }

        private async Task<bool> HasGoals(int playerId)
        {
            var goals = await repository.AllReadonly<Goal>().Where(g => g.GoalScorerId == playerId).ToListAsync();

            return goals.Any();
        }

        private async Task<bool> HasAssists(int playerId)
        {
            var goals = await repository.AllReadonly<Goal>().Where(g => g.GoalAssistantId == playerId).ToListAsync();

            return goals.Any();
        }

        public async Task<List<PlayerSimpleViewModel>> GetAllPlayersAsync()
        {
            var players = await repository.AllReadonly<Player>()
                            .Select(p => new PlayerSimpleViewModel
                            {
                                Name = $"{p.FirstName} {p.LastName}",
                                PlayerId = p.Id,
                                TeamId = p.TeamId
                            })
                            .ToListAsync();

            return players;
        }

        public async Task<PlayerSquadViewModel> GetPlayerDetailsAsync(int playerId)
        {
            var player = await repository.AllReadonly<Player>()
                .Where(p => p.Id == playerId)
                .Select(p => new PlayerSquadViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth.ToString(DateFormat),
                    Position = (PlayerPosition)p.Position,
                    Price = p.Price,
                    Salary = p.Salary,
                    GoalAssited = p.GoalsAssisted.Count,
                    GoalScored = p.GoalsScored.Count,
                })
                .FirstOrDefaultAsync();

            return player;
        }

        public async Task<List<PlayerWidgetViewModel>> GetTopScorersAsync()
        {
            var scorers = await repository.AllReadonly<Player>()
                .Select(p => new PlayerWidgetViewModel
                {
                    PlayerId = p.Id,
                    Name = $"{p.FirstName} {p.LastName}",
                    Position = (PlayerPosition)p.Position,
                    League = p.Team.League.Name,
                    Team = p.Team.Name,
                    GoalsContributed = p.GoalsScored.Count
                })
                .OrderByDescending(p => p.GoalsContributed)
                .Take(5)
                .ToListAsync();

            return scorers;
        }

        public async Task<List<PlayerWidgetViewModel>> GetTopAssistersAsync()
        {
            var assisters = await repository.AllReadonly<Player>()
                .Select(p => new PlayerWidgetViewModel
                {
                    PlayerId = p.Id,
                    Name = $"{p.FirstName} {p.LastName}",
                    Position = (PlayerPosition)p.Position,
                    League = p.Team.League.Name,
                    Team = p.Team.Name,
                    GoalsContributed = p.GoalsAssisted.Count
                })
                .OrderByDescending(p => p.GoalsContributed)
                .Take(5)
                .ToListAsync();

            return assisters;
        }
    }
}
