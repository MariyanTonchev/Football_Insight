using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Services;
using static Football_Insight.Core.Constants.GlobalConstants;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using DataModels = Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Tests.Player
{
    public class PlayerServiceTests
    {
        private readonly Mock<IRepository> mockRepository;
        private readonly PlayerService playerService;

        public PlayerServiceTests()
        {
            mockRepository = new Mock<IRepository>();
            playerService = new PlayerService(mockRepository.Object);
        }

        //Player creation
        [Fact]
        public async Task CreatePlayerAsync_Successful_ReturnsSuccessResult()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                SelectedPosition = 1,
                Price = 1000000m,
                Salary = 50000m,
                TeamId = 1
            };

            // Setup the repository mock
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<DataModels.Player>())).Verifiable();
            mockRepository.Setup(repo => repo.SaveChangesAsync()).Verifiable();

            // Act
            var result = await playerService.CreatePlayerAsync(viewModel);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Successfully created player.", result.Message);
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<DataModels.Player>()), Times.Once);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreatePlayerAsync_MissingPosition_ReturnsFailureResult()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                SelectedPosition = -1,
                Price = 1000000m,
                Salary = 50000m,
                TeamId = 1
            };

            // Act
            var result = await playerService.CreatePlayerAsync(viewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Position is required.", result.Message);
        }

        [Fact]
        public async Task UpdatePlayerAsync_PlayerExists_UpdatesAndSaves()
        {
            // Arrange
            var playerId = 1;
            var mockPlayer = new DataModels.Player
            {
                FirstName = "Original",
                LastName = "Player",
                DateOfBirth = DateTime.Today.AddYears(-20),
                Salary = 100000,
                Position = 1,
                TeamId = 1,
                Price = 500000
            };

            var viewModel = new PlayerFormViewModel
            {
                FirstName = "Updated",
                LastName = "Player",
                DateOfBirth = DateTime.Today.AddYears(-22),
                Salary = 120000,
                SelectedPosition = 2,
                TeamId = 2,
                Price = 550000
            };

            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync(mockPlayer);
            mockRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await playerService.UpdatePlayerAsync(viewModel, playerId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Player edited successfully!", result.Message);
            Assert.Equal("Updated", mockPlayer.FirstName);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePlayerAsync_PlayerNotFound_ReturnsError()
        {
            // Arrange
            var playerId = 99999;
            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync((DataModels.Player)null);

            // Act
            var result = await playerService.UpdatePlayerAsync(new PlayerFormViewModel(), playerId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Player not found!", result.Message);
        }

        [Fact]
        public async Task DeletePlayerAsync_PlayerNotFound_ReturnsError()
        {
            // Arrange
            var playerId = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync((DataModels.Player)null);

            // Act
            var result = await playerService.DeletePlayerAsync(playerId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Player not found.", result.Message);
        }

        [Fact]
        public async Task DeletePlayerAsync_SuccessfulDeletion_ReturnsSuccessMessage()
        {
            // Arrange
            var playerId = 1;
            var mockPlayer = new DataModels.Player { Id = playerId, FirstName = "Test", LastName = "Testov" };

            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync(mockPlayer);
            mockRepository.Setup(repo => repo.AllReadonly<Goal>()).Returns(new List<Goal>().AsAsyncQueryable());
            mockRepository.Setup(repo => repo.RemoveAsync(mockPlayer)).Returns(Task.CompletedTask);
            mockRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await playerService.DeletePlayerAsync(playerId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal($"Successfully deleted player with ID:1 and Name: Test Testov!", result.Message);
            mockRepository.Verify(repo => repo.RemoveAsync(mockPlayer), Times.Once);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePlayerAsync_FailsOnHasGoals_ReturnsErrorMessage()
        {
            // Arrange
            var playerId = 1;
            var mockPlayer = new DataModels.Player { Id = playerId, FirstName = "Test", LastName = "Testov" };

            var goals = new List<Goal>
                        {
                            new Goal { Id = 1, GoalScorerId = playerId }
                        }.AsAsyncQueryable();

            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync(mockPlayer);
            mockRepository.Setup(repo => repo.AllReadonly<Goal>()).Returns(goals);

            // Act
            var result = await playerService.DeletePlayerAsync(playerId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot delete a player with goals.", result.Message);
        }

        [Fact]
        public async Task DeletePlayerAsync_FailsOnHasAssists_ReturnsErrorMessage()
        {
            // Arrange
            var playerId = 1;
            var mockPlayer1 = new DataModels.Player { Id = playerId, FirstName = "Test", LastName = "Testov" };
            var mockPlayer2 = new DataModels.Player { Id = 2, FirstName = "Test", LastName = "Testov" };

            var goals = new List<Goal>
                        {
                            new Goal { Id = 1, GoalScorerId = 2, GoalAssistantId = playerId }
                        }.AsAsyncQueryable();

            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync(mockPlayer1);
            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.Player>(playerId)).ReturnsAsync(mockPlayer2);
            mockRepository.Setup(repo => repo.AllReadonly<Goal>()).Returns(goals);

            // Act
            var result = await playerService.DeletePlayerAsync(playerId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot delete a player with assists.", result.Message);
        }

        [Fact]
        public async Task GetPlayerDetailsAsync_ReturnsCorrectPlayerDetails()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var playerId = 1;
            var players = new List<DataModels.Player>
                            {
                                new DataModels.Player
                                {
                                    Id = playerId,
                                    FirstName = "John",
                                    LastName = "Doe",
                                    DateOfBirth = new DateTime(1990, 01, 01),
                                    Position = 1,
                                    Price = 100000m,
                                    Salary = 5000m,
                                    GoalsAssisted = new List<Goal>(),
                                    GoalsScored = new List<Goal>()
                                }
                            }.AsAsyncQueryable();

            var mockSet = new Mock<DbSet<DataModels.Player>>();
            mockSet.As<IQueryable<DataModels.Player>>().Setup(m => m.Provider).Returns(players.Provider);
            mockSet.As<IQueryable<DataModels.Player>>().Setup(m => m.Expression).Returns(players.Expression);
            mockSet.As<IQueryable<DataModels.Player>>().Setup(m => m.ElementType).Returns(players.ElementType);
            mockSet.As<IQueryable<DataModels.Player>>().Setup(m => m.GetEnumerator()).Returns(players.GetEnumerator());

            mockRepository.Setup(repo => repo.AllReadonly<DataModels.Player>()).Returns(mockSet.Object);

            var service = new PlayerService(mockRepository.Object);

            // Act
            var result = await service.GetPlayerDetailsAsync(playerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerId, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal(new DateTime(1990, 01, 01).ToString(DateFormat), result.DateOfBirth);
            Assert.Equal(PlayerPosition.Defender, result.Position);
            Assert.Equal(100000m.ToString(CurrencyFormat), result.Price);
            Assert.Equal(5000m.ToString(CurrencyFormat), result.Salary);
            Assert.Equal(0, result.GoalAssited);
            Assert.Equal(0, result.GoalScored);
        }

        [Fact]
        public async Task GetTopScorersAsync_ReturnsTopScorersOrderedByGoals()
        {
            // Arrange
            var players = new List<DataModels.Player>
                            {
                                new DataModels.Player { Id = 1, FirstName = "John", LastName = "Doe", Position = (int)PlayerPosition.Forward, Team = new DataModels.Team { Id = 1, Name = "Team A", League = new DataModels.League { Id = 1, Name = "League A" }, HomeMatches = new List<DataModels.Match>(), AwayMatches = new List<DataModels.Match>() }, GoalsScored = new List<Goal> { new Goal(), new Goal() } },
                                new DataModels.Player { Id = 2, FirstName = "Jane", LastName = "Smith", Position = (int)PlayerPosition.Midfielder, Team = new DataModels.Team { Id = 1, Name = "Team A", League = new DataModels.League { Id = 1, Name = "League A" }, HomeMatches = new List<DataModels.Match>(), AwayMatches = new List<DataModels.Match>() }, GoalsScored = new List<Goal> { new Goal() } },
                                new DataModels.Player { Id = 3, FirstName = "Alice", LastName = "Johnson", Position = (int)PlayerPosition.Defender, Team = new DataModels.Team { Id = 2, Name = "Team B", League = new DataModels.League { Id = 1, Name = "League A" }, HomeMatches = new List<DataModels.Match>(), AwayMatches = new List<DataModels.Match>() }, GoalsScored = new List<Goal> { new Goal(), new Goal(), new Goal() } }
                            }.AsAsyncQueryable();

            var playersQueryable = players.AsQueryable();

            mockRepository.Setup(repo => repo.AllReadonly<DataModels.Player>()).Returns(playersQueryable);

            // Act
            var result = await playerService.GetTopScorersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(3, result[0].GoalsContributed); // Alice Johnson with 3 goals
            Assert.Equal(2, result[1].GoalsContributed); // John Doe with 2 goals
            Assert.Equal(1, result[2].GoalsContributed); // Jane Smith with 1 goal
            Assert.Equal("Alice Johnson", result[0].Name);
            Assert.Equal("John Doe", result[1].Name);
            Assert.Equal("Jane Smith", result[2].Name);
        }

        [Fact]
        public async Task GetTopAssistersAsync_ReturnsTopAssistersOrderedByAssists()
        {
            // Arrange
            var players = new List<DataModels.Player>
                                {
                                    new DataModels.Player { Id = 1, FirstName = "John", LastName = "Doe", Position = (int)PlayerPosition.Forward, Team = new DataModels.Team { Id = 1, Name = "Team A", League = new DataModels.League { Id = 1, Name = "League A" }}, GoalsAssisted = new List<Goal> { new Goal(), new Goal() }},
                                    new DataModels.Player { Id = 2, FirstName = "Jane", LastName = "Smith", Position = (int)PlayerPosition.Midfielder, Team = new DataModels.Team { Id = 1, Name = "Team A", League = new DataModels.League { Id = 1, Name = "League A" }}, GoalsAssisted = new List<Goal> { new Goal() }},
                                    new DataModels.Player { Id = 3, FirstName = "Alice", LastName = "Johnson", Position = (int)PlayerPosition.Defender, Team = new DataModels.Team { Id = 2, Name = "Team B", League = new DataModels.League { Id = 1, Name = "League A" }}, GoalsAssisted = new List<Goal> { new Goal(), new Goal(), new Goal() }}
                                }.AsAsyncQueryable();

            mockRepository.Setup(repo => repo.AllReadonly<DataModels.Player>()).Returns(players);

            // Act
            var result = await playerService.GetTopAssistersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(3, result[0].GoalsContributed); // Alice Johnson with 3 assists
            Assert.Equal(2, result[1].GoalsContributed); // John Doe with 2 assists
            Assert.Equal(1, result[2].GoalsContributed); // Jane Smith with 1 assist
            Assert.Equal("Alice Johnson", result[0].Name);
            Assert.Equal("John Doe", result[1].Name);
            Assert.Equal("Jane Smith", result[2].Name);
        }
    }
}