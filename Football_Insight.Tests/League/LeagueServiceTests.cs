using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;
using Football_Insight.Core.Services;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using DataModels = Football_Insight.Infrastructure.Data.Models;


namespace Football_Insight.Tests.League
{
    public class LeagueServiceTests
    {
        private readonly Mock<IRepository> mockRepo;
        private readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private readonly LeagueService leagueService;

        public LeagueServiceTests()
        {
            mockRepo = new Mock<IRepository>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            leagueService = new LeagueService(mockRepo.Object, mockHttpContextAccessor.Object);        
        }

        [Fact]
        public async Task CreateLeagueAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var model = new LeagueFormViewModel { Name = "Premier League" };
            var existingLeagues = new List<DataModels.League>
                    {
                        new DataModels.League { Name = "Premier League" }
                    }.AsAsyncQueryable();

            mockRepo.Setup(x => x.AllReadonly<DataModels.League>()).Returns(existingLeagues);

            // Act
            var result = await leagueService.CreateLeagueAsync(model);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("A league with the same name already exists.", result.Message);
        }

        [Fact]
        public async Task CreateLeagueAsync_ValidData_ReturnsSuccessWithId()
        {
            // Arrange
            var model = new LeagueFormViewModel { Name = "La Liga" };
            var existingLeagues = new List<DataModels.League>().AsAsyncQueryable();

            mockRepo.Setup(x => x.AllReadonly<DataModels.League>()).Returns(existingLeagues);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<DataModels.League>())).Callback<DataModels.League>(league => league.Id = 1);
            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await leagueService.CreateLeagueAsync(model);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("League created successfully.", result.Message);
            Assert.Equal(1, result.ObjectId);
        }

        [Fact]
        public async Task UpdateLeagueAsync_LeagueNotFound_ReturnsError()
        {
            // Arrange
            var viewModel = new LeagueFormViewModel { Id = 1, Name = "Premier League" };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(viewModel.Id)).ReturnsAsync((DataModels.League)null);

            // Act
            var result = await leagueService.UpdateLeagueAsync(viewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("League not found!", result.Message);
        }

        [Fact]
        public async Task UpdateLeagueAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var existingLeague = new DataModels.League { Id = 2, Name = "La Liga" };
            var leagueToUpdate = new DataModels.League { Id = 1, Name = "Bundesliga" };
            var viewModel = new LeagueFormViewModel { Id = 1, Name = "La Liga" };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(viewModel.Id)).ReturnsAsync(leagueToUpdate);
            mockRepo.Setup(x => x.AllReadonly<DataModels.League>()).Returns(new List<DataModels.League> { existingLeague, leagueToUpdate }.AsAsyncQueryable());

            // Act
            var result = await leagueService.UpdateLeagueAsync(viewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("A league with the same name already exists.", result.Message);
        }

        [Fact]
        public async Task UpdateLeagueAsync_Successful_ReturnsSuccess()
        {
            // Arrange
            var leagueToUpdate = new DataModels.League { Id = 1, Name = "Bundesliga" };
            var viewModel = new LeagueFormViewModel { Id = 1, Name = "Bundesliga Updated" };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(viewModel.Id)).ReturnsAsync(leagueToUpdate);
            mockRepo.Setup(x => x.AllReadonly<DataModels.League>()).Returns(new List<DataModels.League> { leagueToUpdate }.AsAsyncQueryable());
            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await leagueService.UpdateLeagueAsync(viewModel);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("League edited successfully.", result.Message);
        }

        [Fact]
        public async Task UpdateLeagueAsync_SaveChangesFails_ReturnsError()
        {
            // Arrange
            var leagueToUpdate = new DataModels.League { Id = 1, Name = "Bundesliga" };
            var viewModel = new LeagueFormViewModel { Id = 1, Name = "Bundesliga Updated" };
            var exceptionMessage = "Database error";

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(viewModel.Id)).ReturnsAsync(leagueToUpdate);
            mockRepo.Setup(x => x.AllReadonly<DataModels.League>()).Returns(new List<DataModels.League> { leagueToUpdate }.AsAsyncQueryable());
            mockRepo.Setup(x => x.SaveChangesAsync()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await leagueService.UpdateLeagueAsync(viewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(exceptionMessage, result.Message);
        }

        [Fact]
        public async Task DeleteLeagueAsync_LeagueNotFound_ReturnsError()
        {
            // Arrange
            int nonExistentLeagueId = 999;

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(nonExistentLeagueId)).ReturnsAsync((DataModels.League)null);

            // Act
            var result = await leagueService.DeleteLeagueAsync(nonExistentLeagueId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("League not found!", result.Message);
        }

        [Fact]
        public async Task FindLeagueAsync_LeagueFound_ReturnsViewModel()
        {
            // Arrange
            var expectedLeague = new DataModels.League { Id = 1, Name = "Premier League" };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(1)).ReturnsAsync(expectedLeague);

            // Act
            var result = await leagueService.FindLeagueAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLeague.Id, result.Id);
            Assert.Equal(expectedLeague.Name, result.Name);
        }

        [Fact]
        public async Task GetAllLeaguesAsync_ReturnsAllLeagues()
        {
            // Arrange
            var leagues = new List<DataModels.League>
                            {
                                new DataModels.League { Id = 1, Name = "Premier League" },
                                new DataModels.League { Id = 2, Name = "La Liga" }
                            };

            var leaguesQueryable = leagues.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.AllReadonly<DataModels.League>()).Returns(leaguesQueryable);

            // Act
            var result = await leagueService.GetAllLeaguesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(leagues.Count, result.Count);
            Assert.Collection(result,
                item => Assert.Equal("Premier League", item.Name),
                item => Assert.Equal("La Liga", item.Name));
        }

        [Fact]
        public async Task GetAllLeaguesAsync_NoLeagues_ReturnsEmptyList()
        {
            // Arrange
            var leagues = new List<DataModels.League>();

            var leaguesQueryable = leagues.AsAsyncQueryable();
            mockRepo.Setup(repo => repo.AllReadonly<DataModels.League>()).Returns(leaguesQueryable);

            // Act
            var result = await leagueService.GetAllLeaguesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllLeaguesWithTeamsAsync_ReturnsLeaguesWithTeams()
        {
            // Arrange
            var leagues = new List<DataModels.League>
                            {
                                new DataModels.League { Id = 1, Name = "Premier League" },
                                new DataModels.League { Id = 2, Name = "La Liga" }
                            };
                                    var teams = new List<DataModels.Team>
                            {
                                new DataModels.Team { Id = 1, Name = "Team A", LeagueId = 1 },
                                new DataModels.Team { Id = 2, Name = "Team B", LeagueId = 1 },
                                new DataModels.Team { Id = 3, Name = "Team C", LeagueId = 2 }
                            };

            var leaguesQueryable = leagues.AsAsyncQueryable();
            var teamsQueryable = teams.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.All<DataModels.League>()).Returns(leaguesQueryable);
            mockRepo.Setup(repo => repo.All<DataModels.Team>()).Returns(teamsQueryable);

            // Act
            var result = await leagueService.GetAllLeaguesWithTeamsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(leagues.Count, result.Count);
            Assert.Collection(result,
                league =>
                {
                    Assert.Equal("Premier League", league.Name);
                    Assert.Equal(2, league.Teams.Count);
                },
                league =>
                {
                    Assert.Equal("La Liga", league.Name);
                    Assert.Single(league.Teams);
                });
        }

        [Fact]
        public async Task GetAllLeaguesWithTeamsAsync_NoLeagues_ReturnsEmptyList()
        {
            // Arrange
            var leagues = new List<DataModels.League>(); 
            var teams = new List<DataModels.Team>(); 

            var leaguesQueryable = leagues.AsAsyncQueryable();
            var teamsQueryable = teams.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.All<DataModels.League>()).Returns(leaguesQueryable);
            mockRepo.Setup(repo => repo.All<DataModels.Team>()).Returns(teamsQueryable);

            // Act
            var result = await leagueService.GetAllLeaguesWithTeamsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllTeamsAsync_ValidLeagueId_ReturnsTeams()
        {
            // Arrange
            int leagueId = 1;
            var teams = new List<DataModels.Team>
                            {
                                new DataModels.Team { Id = 1, Name = "Team A", LeagueId = leagueId },
                                new DataModels.Team { Id = 2, Name = "Team B", LeagueId = leagueId }
                            };

            var teamsQueryable = teams.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.AllReadonly(It.IsAny<Expression<Func<DataModels.Team, bool>>>()))
                    .Returns(teamsQueryable.Where(t => t.LeagueId == leagueId));

            // Act
            var result = await leagueService.GetAllTeamsAsync(leagueId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teams.Count, result.Count);
            Assert.All(result, model => Assert.Contains(model.Name, teams.Select(t => t.Name)));
        }

        [Fact]
        public async Task GetAllTeamsAsync_NoTeamsForLeague_ReturnsEmptyList()
        {
            // Arrange
            int leagueId = 99; 

            var teams = new List<DataModels.Team>(); // Empty list of teams
            var teamsQueryable = teams.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.AllReadonly(It.IsAny<Expression<Func<DataModels.Team, bool>>>()))
                    .Returns(teamsQueryable.Where(t => t.LeagueId == leagueId));

            // Act
            var result = await leagueService.GetAllTeamsAsync(leagueId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetLeagueDetailsAsync_LeagueFound_ReturnsViewModel()
        {
            // Arrange
            int leagueId = 1;
            var expectedLeague = new DataModels.League { Id = leagueId, Name = "Premier League" };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.League>(leagueId)).ReturnsAsync(expectedLeague);

            // Act
            var result = await leagueService.GetLeagueDetailsAsync(leagueId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLeague.Id, result.Id);
            Assert.Equal(expectedLeague.Name, result.Name);
        }

        [Fact]
        public async Task GetTeamTableAsync_ValidLeagueId_ReturnsOrderedTeamTable()
        {
            // Arrange
            int leagueId = 1;
            var teams = new List<DataModels.Team>
                    { 
                        new DataModels.Team
                        {
                            Id = 1,
                            Name = "Team A",
                            LeagueId = leagueId,
                            HomeMatches = new List<DataModels.Match>
                            {
                                new DataModels.Match { HomeScore = 2, AwayScore = 1, Status = MatchStatus.Finished },
                                new DataModels.Match { HomeScore = 0, AwayScore = 0, Status = MatchStatus.Finished }
                            },
                            AwayMatches = new List<DataModels.Match>
                            {
                                new DataModels.Match { HomeScore = 0, AwayScore = 1, Status = MatchStatus.Finished },
                                new DataModels.Match { HomeScore = 1, AwayScore = 1, Status = MatchStatus.Finished }
                            }
                        }
                    }.AsAsyncQueryable();

            mockRepo.Setup(repo => repo.AllReadonly(It.IsAny<Expression<Func<DataModels.Team, bool>>>())).Returns(teams);

            // Act
            var result = await leagueService.GetTeamTableAsync(leagueId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var teamTable = result.First();
            Assert.Equal(1, teamTable.Id);
            Assert.Equal("Team A", teamTable.Name);
            Assert.Equal(2, teamTable.Wins);// 
            Assert.Equal(2, teamTable.Draws); // 2 wins, 2 draws, 0 losses
            Assert.Equal(0, teamTable.Losses); // 
            Assert.Equal(4, teamTable.GoalsFor); // 2 home goals + 2 away goals (4 goals)
            Assert.Equal(2, teamTable.GoalsAgainst); // 1 on home games + 1 on away games (2 goals)
            Assert.Equal(8, teamTable.Points);  // 2 wins (6 points) + 2 draws (2 points)
        }

        [Fact]
        public async Task GetTeamTableAsync_NoTeamsForLeague_ReturnsEmptyList()
        {
            // Arrange
            int leagueId = 99; 

            var teams = new List<DataModels.Team>().AsAsyncQueryable();

            mockRepo.Setup(repo => repo.AllReadonly(It.IsAny<Expression<Func<DataModels.Team, bool>>>())).Returns(teams);

            // Act
            var result = await leagueService.GetTeamTableAsync(leagueId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
