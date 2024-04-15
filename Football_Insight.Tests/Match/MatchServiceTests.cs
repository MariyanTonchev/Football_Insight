using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Services;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using static Football_Insight.Core.Constants.GlobalConstants;
using DataModels = Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Tests.Match
{

    public class MatchServiceTests
    {
        private readonly Mock<IRepository> mockRepo;
        private readonly Mock<IStadiumService> mockStadiumService;
        private readonly Mock<ILeagueService> mockLeagueService;
        private readonly Mock<ITeamService> mockTeamService;
        private readonly Mock<IMatchTimerService> mockMatchTimerService;
        private readonly Mock<IMatchJobService> mockMatchJobService;
        private readonly Mock<IMemoryCache> mockMemoryCache;
        private readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private readonly Mock<IGoalService> mockGoalService;
        private readonly MatchService matchService;

        public MatchServiceTests()
        {
            mockRepo = new Mock<IRepository>();
            mockStadiumService = new Mock<IStadiumService>();
            mockLeagueService = new Mock<ILeagueService>();
            mockTeamService = new Mock<ITeamService>();
            mockMatchTimerService = new Mock<IMatchTimerService>();
            mockMatchJobService = new Mock<IMatchJobService>();
            mockMemoryCache = new Mock<IMemoryCache>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockGoalService = new Mock<IGoalService>();
            matchService = new MatchService(mockRepo.Object, mockStadiumService.Object, mockLeagueService.Object, mockTeamService.Object, mockGoalService.Object,
                mockMatchTimerService.Object, mockMatchJobService.Object, mockMemoryCache.Object, mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task CreateMatchAsync_Successful_ReturnsMatchId()
        {
            // Arrange
            var matchFormViewModel = new MatchFormViewModel
            {
                DateAndTime = DateTime.Now.AddDays(1),
                HomeTeamId = 1,
                AwayTeamId = 2,
            };
            int leagueId = 1;
            int expectedStadiumId = 3;
            int expectedMatchId = 10;

            mockStadiumService.Setup(x => x.GetStadiumIdAsync(matchFormViewModel.HomeTeamId)).ReturnsAsync(expectedStadiumId);
            mockRepo.Setup(x => x.AddAsync(It.IsAny<DataModels.Match>())).Callback<DataModels.Match>(match =>
            {
                match.Id = expectedMatchId;
            });
            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await matchService.CreateMatchAsync(matchFormViewModel, leagueId);

            // Assert
            Assert.Equal(expectedMatchId, result);
            mockRepo.Verify(x => x.AddAsync(It.Is<DataModels.Match>(m =>
                m.HomeTeamId == matchFormViewModel.HomeTeamId &&
                m.AwayTeamId == matchFormViewModel.AwayTeamId &&
                m.LeagueId == leagueId &&
                m.StadiumId == expectedStadiumId &&
                m.DateAndTime == matchFormViewModel.DateAndTime &&
                m.Status == MatchStatus.Scheduled
            )), Times.Once);
            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateMatchAsync_StatusNotScheduled_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Finished };
            var model = new MatchFormViewModel { HomeTeamId = 1, AwayTeamId = 2, DateAndTime = DateTime.Now.AddDays(1) };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.UpdateMatchAsync(model, 1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot edit a match that has started, finished, or been postponed.", result.Message);
        }

        [Fact]
        public async Task UpdateMatchAsync_ValidConditions_ReturnsSuccess()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2, Status = MatchStatus.Scheduled };
            var model = new MatchFormViewModel { HomeTeamId = 3, AwayTeamId = 4, DateAndTime = DateTime.Now.AddDays(1) };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await matchService.UpdateMatchAsync(model, 1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Match edited successfully!", result.Message);
            Assert.Equal(3, match.HomeTeamId);
            Assert.Equal(4, match.AwayTeamId);
            Assert.Equal(model.DateAndTime, match.DateAndTime);
        }

        [Fact]
        public async Task DeleteMatchAsync_MatchNotFound_ReturnsError()
        {
            // Arrange
            int nonExistentMatchId = 1;

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(nonExistentMatchId)).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.DeleteMatchAsync(nonExistentMatchId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match not found!", result.Message);
        }

        [Fact]
        public async Task DeleteMatchAsync_MatchFinished_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Finished };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.DeleteMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot delete a match that has started, finished, or been postponed.", result.Message);
        }

        [Fact]
        public async Task DeleteMatchAsync_MatchPosponed_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Postponed };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.DeleteMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot delete a match that has started, finished, or been postponed.", result.Message);
        }

        [Fact]
        public async Task DeleteMatchAsync_MatchStarted_ReturnsError()
        {
            // Arrange
            var match1 = new DataModels.Match { Id = 1, Status = MatchStatus.FirstHalf };
            var match2 = new DataModels.Match { Id = 2, Status = MatchStatus.HalfTime };
            var match3 = new DataModels.Match { Id = 3, Status = MatchStatus.SecondHalf };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match1);
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(2)).ReturnsAsync(match2);
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(3)).ReturnsAsync(match3);

            // Act
            var result1 = await matchService.DeleteMatchAsync(1);
            var result2 = await matchService.DeleteMatchAsync(2);
            var result3 = await matchService.DeleteMatchAsync(3);

            // Assert
            Assert.False(result1.Success);
            Assert.Equal("You cannot delete a match that has started, finished, or been postponed.", result1.Message);
            Assert.False(result2.Success);
            Assert.Equal("You cannot delete a match that has started, finished, or been postponed.", result2.Message);
            Assert.False(result3.Success);
            Assert.Equal("You cannot delete a match that has started, finished, or been postponed.", result3.Message);
        }

        [Fact]
        public async Task DeleteMatchAsync_ValidConditions_ReturnsSuccess()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Scheduled };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockRepo.Setup(x => x.RemoveAsync(match)).Returns(Task.CompletedTask);
            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await matchService.DeleteMatchAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal($"Successfully deleted {match.Id}!", result.Message);
            mockRepo.Verify(x => x.RemoveAsync(match), Times.Once);
            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task StartMatchAsync_MatchNotFound_ReturnsError()
        {
            int matchId = 1;
            mockRepo.Setup(r => r.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.StartMatchAsync(matchId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match not found.", result.Message);
        }

        [Fact]
        public async Task StartMatchAsync_MatchAlreadyInProgress_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.FirstHalf };

            mockRepo.Setup(r => r.GetByIdAsync<DataModels.Match>(match.Id)).ReturnsAsync(match);

            // Act
            var result = await matchService.StartMatchAsync(match.Id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match is already in progress.", result.Message);
        }

        [Fact]
        public async Task StartMatchAsync_MatchNotScheduled_ReturnsError()
        {
            // Arrange
            int matchId = 1;
            var match = new DataModels.Match { Id = matchId, Status = MatchStatus.Finished };

            mockRepo.Setup(r => r.GetByIdAsync<DataModels.Match>(matchId)).ReturnsAsync(match);

            // Act
            var result = await matchService.StartMatchAsync(matchId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match is not in the scheduled status.", result.Message);
        }

        [Fact]
        public async Task StartMatchAsync_ExceptionThrown_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(r => r.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ThrowsAsync(new Exception());
            
            // Act
            var result = await matchService.StartMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error occurred while starting the match.", result.Message);
        }

        [Fact]
        public async Task PauseMatchAsync_MatchNotFound_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.PauseMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match not found.", result.Message);
        }

        [Fact]
        public async Task PauseMatchAsync_TooEarlyForHalftime_ReturnsError()
        {
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.FirstHalf };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockMatchTimerService.Setup(x => x.GetMatchMinute(1)).Returns(HalfTimeMinute - 1);

            var result = await matchService.PauseMatchAsync(1);

            Assert.False(result.Success);
            Assert.Equal("The match is too early for half time.", result.Message);
        }

        [Fact]
        public async Task PauseMatchAsync_InvalidStatus_ReturnsError()
        {
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Finished };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockMatchTimerService.Setup(x => x.GetMatchMinute(1)).Returns(FullTimeMinute + 1);

            var result = await matchService.PauseMatchAsync(1);

            Assert.False(result.Success);
            Assert.Equal("The match is finished.", result.Message);
        }

        [Fact]
        public async Task PauseMatchAsync_ExceptionThrown_ReturnsError()
        {
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await matchService.PauseMatchAsync(1);

            Assert.False(result.Success);
            Assert.Equal("An error occurred while pausing the match.", result.Message);
        }

        [Fact]
        public async Task UnpauseMatchAsync_MatchNotFound_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.UnpauseMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match not found.", result.Message);
        }

        [Fact]
        public async Task UnpauseMatchAsync_AlreadyInSecondHalf_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.SecondHalf };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.UnpauseMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Second half is already in progress.", result.Message);
        }

        [Fact]
        public async Task UnpauseMatchAsync_NotAtHalftime_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Scheduled };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.UnpauseMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("The match is not at half time.", result.Message);
        }

        [Fact]
        public async Task UnpauseMatchAsync_ExceptionThrown_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            var result = await matchService.UnpauseMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error occurred while unpausing the match.", result.Message);
        }

        [Fact]
        public async Task EndMatchAsync_MatchNotFound_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.EndMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Match not found.", result.Message);
        }

        [Fact]
        public async Task EndMatchAsync_MatchAlreadyEndedOrPostponed_ReturnsError()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.Finished };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);

            // Act
            var result = await matchService.EndMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("The match is already finished.", result.Message);
        }

        [Fact]
        public async Task EndMatchAsync_MatchPostponed_ReturnsSuccess()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.FirstHalf };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockMatchTimerService.Setup(x => x.GetMatchMinute(1)).Returns(FullTimeMinute - 10);

            // Act
            var result = await matchService.EndMatchAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Match Postponed successfully!", result.Message);
        }

        [Fact]
        public async Task EndMatchAsync_Successful_ReturnsSuccess()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Status = MatchStatus.SecondHalf };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(1)).ReturnsAsync(match);
            mockMatchTimerService.Setup(x => x.GetMatchMinute(1)).Returns(FullTimeMinute);
            mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            mockMatchJobService.Setup(j => j.EndMatchJobAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await matchService.EndMatchAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Match Finished successfully!", result.Message);
        }

        [Fact]
        public async Task EndMatchAsync_ExceptionThrown_ReturnsError()
        {
            // Arrange
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            var result = await matchService.EndMatchAsync(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error occurred while ending the match.", result.Message);
        }

        [Fact]
        public async Task GetMatchFormViewModelByIdAsync_MatchNotFound_ReturnsNull()
        {
            // Arrange
            int nonExistentMatchId = 1;

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(nonExistentMatchId)).ReturnsAsync((DataModels.Match)null);

            // Act
            var result = await matchService.GetMatchFormViewModelByIdAsync(nonExistentMatchId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetMatchSimpleViewAsync_ValidMatch_ReturnsViewModel()
        {
            // Arrange
            int matchId = 1;
            var match = new DataModels.Match
            {
                Id = matchId,
                HomeTeamId = 1,
                AwayTeamId = 2,
                LeagueId = 3,
                DateAndTime = DateTime.Now.AddDays(1)  // Example Date and Time
            };

            // Setup mocks
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(matchId)).ReturnsAsync(match);
            mockTeamService.Setup(x => x.GetTeamNameAsync(match.HomeTeamId)).ReturnsAsync("Home Team Name");
            mockTeamService.Setup(x => x.GetTeamNameAsync(match.AwayTeamId)).ReturnsAsync("Away Team Name");

            // Act
            var result = await matchService.GetMatchSimpleViewAsync(matchId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(matchId, result.MatchId);
            Assert.Equal("Home Team Name", result.HomeTeam);
            Assert.Equal("Away Team Name", result.AwayTeam);
            Assert.Equal(match.LeagueId, result.LeagueId);
        }

        [Fact]
        public async Task GetMatchEndViewAsync_ValidMatch_ReturnsViewModel()
        {
            // Arrange
            int matchId = 1;
            var match = new DataModels.Match
            {
                Id = matchId,
                HomeTeamId = 1,
                AwayTeamId = 2,
                LeagueId = 3,
                Status = MatchStatus.SecondHalf
            };

            // Setup mocks
            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(matchId)).ReturnsAsync(match);
            mockTeamService.Setup(x => x.GetTeamNameAsync(match.HomeTeamId)).ReturnsAsync("Home Team Name");
            mockTeamService.Setup(x => x.GetTeamNameAsync(match.AwayTeamId)).ReturnsAsync("Away Team Name");
            mockMatchTimerService.Setup(x => x.GetMatchMinute(matchId)).Returns(45);  // Assuming 45 minutes for simplicity

            // Act
            var result = await matchService.GetMatchEndViewAsync(matchId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(matchId, result.MatchId);
            Assert.Equal("Home Team Name", result.HomeTeam);
            Assert.Equal("Away Team Name", result.AwayTeam);
            Assert.Equal(match.LeagueId, result.LeagueId);
            Assert.Equal(45, result.MatchMinute);
            Assert.Equal(MatchStatus.SecondHalf, result.MatchStatus);
        }

        [Fact]
        public async Task GetMatchMinuteAsync_ValidMatch_ReturnsMatchMinute()
        {
            // Arrange
            int matchId = 1;
            var match = new DataModels.Match
            {
                Id = matchId,
                Minutes = 90 
            };

            mockRepo.Setup(x => x.GetByIdAsync<DataModels.Match>(matchId)).ReturnsAsync(match);

            // Act
            var result = await matchService.GetMatchMinuteAsync(matchId);

            // Assert
            Assert.Equal(90, result);
        }
    }
}
