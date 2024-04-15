using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Team;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Services;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Moq;
using DataModels = Football_Insight.Infrastructure.Data.Models;
using Football_Insight.Core.Models.Stadium;
using MockQueryable.Moq;
using System.Linq.Expressions;

namespace Football_Insight.Tests.Team
{
    public class TeamServiceTests
    {
        private readonly Mock<IRepository> mockRepository;
        private readonly Mock<ILeagueService> mockLeagueService;
        private readonly Mock<IStadiumService> mockStadiumService;
        private readonly TeamService teamService;

        public TeamServiceTests()
        {
            mockRepository = new Mock<IRepository>();
            mockStadiumService = new Mock<IStadiumService>();
            mockLeagueService = new Mock<ILeagueService>();
            teamService = new TeamService(mockRepository.Object, mockStadiumService.Object, mockLeagueService.Object);
        }

        [Fact]
        public async Task CreateTeamAsync_ValidInput_ProcessesSuccessfully()
        {
            // Arrange
            var viewModel = new TeamFormViewModel
            {
                Name = "New Team",
                Founded = DateTime.Now.Year,
                LogoURL = "http://example.com/logo.png",
                Coach = "John Doe",
                StadiumId = 1,
                LeagueId = 1
            };

            mockRepository.Setup(repo => repo.GetByIdAsync<DataModels.League>(viewModel.LeagueId))
                .ReturnsAsync(new DataModels.League()); 

            mockRepository.Setup(repo => repo.GetByIdAsync<Stadium>(viewModel.StadiumId))
                .ReturnsAsync(new Stadium());

            // Act & Assert
            var exception = await Record.ExceptionAsync(() => teamService.CreateTeamAsync(viewModel));
            Assert.Null(exception);
        }

        [Fact]
        public async Task CreateTeamAsync_ReturnsSuccessAndCorrectMessage()
        {
            // Arrange
            var viewModel = new TeamFormViewModel
            {
                Name = "New Team",
                Founded = DateTime.Now.Year,
                LogoURL = "http://example.com/logo.png",
                Coach = "John Doe",
                StadiumId = 1,
                LeagueId = 1
            };

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<DataModels.Team>())).Verifiable();
            mockRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await teamService.CreateTeamAsync(viewModel);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Successfully created team.", result.Message);
            Assert.NotNull(result.ObjectId);

            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<DataModels.Team>()), Times.Once);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateTeamAsync_NullViewModel_ThrowsNullReferenceException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => teamService.CreateTeamAsync(null));
        }

        [Fact]
        public async Task CreateTeamAsync_ValidInput_CorrectDataMapping()
        {
            // Arrange
            var viewModel = new TeamFormViewModel
            {
                Name = "Mapped Team",
                Founded = 1999,
                LogoURL = "http://example.com/mappedlogo.png",
                Coach = "Mapped Coach",
                StadiumId = 3,
                LeagueId = 4
            };

            DataModels.Team savedTeam = null;
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<DataModels.Team>()))
                .Callback<DataModels.Team>(team => savedTeam = team)
                .Returns(Task.CompletedTask);

            mockRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            await teamService.CreateTeamAsync(viewModel);

            // Assert
            Assert.NotNull(savedTeam);
            Assert.Equal(viewModel.Name, savedTeam.Name);
            Assert.Equal(viewModel.Founded, savedTeam.Founded);
            Assert.Equal(viewModel.LogoURL, savedTeam.LogoURL);
            Assert.Equal(viewModel.Coach, savedTeam.Coach);
            Assert.Equal(viewModel.StadiumId, savedTeam.StadiumId);
            Assert.Equal(viewModel.LeagueId, savedTeam.LeagueId);
        }

        [Fact]
        public async Task GetTeamSimpleViewModelAsync_TeamExists_ReturnsViewModel()
        {
            // Arrange
            var teamId = 1;
            var team = new DataModels.Team { Id = teamId, Name = "FC Awesome" };
            mockRepository.Setup(r => r.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync(team);

            // Act
            var result = await teamService.GetTeamSimpleViewModelAsync(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamId, result.TeamId);
            Assert.Equal("FC Awesome", result.Name);
        }

        [Fact]
        public async Task GetCreateFormViewModel_ReturnsViewModelWithLeaguesAndStadiums()
        {
            // Arrange
            var leagues = new List<LeagueSimpleViewModel>
                    {
                        new LeagueSimpleViewModel { Id = 1, Name = "League One" },
                        new LeagueSimpleViewModel { Id = 2, Name = "League Two" }
                    };

            var stadiums = new List<StadiumSimpleViewModel>
                    {
                        new StadiumSimpleViewModel { Id = 1, Name = "Stadium A" },
                        new StadiumSimpleViewModel { Id = 2, Name = "Stadium B" }
                    };

            mockLeagueService.Setup(s => s.GetAllLeaguesAsync()).ReturnsAsync(leagues);
            mockStadiumService.Setup(s => s.GetAllStadiumAsync()).ReturnsAsync(stadiums);

            // Act
            var result = await teamService.GetCreateFormViewModel();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(leagues, result.Leagues);
            Assert.Equal(stadiums, result.Stadiums);
        }

        [Fact]
        public async Task GetEditFormViewModel_TeamExists_ReturnsFilledViewModel()
        {
            // Arrange
            var teamId = 1;
            var team = new DataModels.Team
            {
                Id = teamId,
                Name = "Test Team",
                Founded = DateTime.Now.Year,
                LogoURL = "http://example.com/logo.png",
                Coach = "Coach Name",
                LeagueId = 1,
                StadiumId = 2
            };
            var leagues = new List<LeagueSimpleViewModel> { new LeagueSimpleViewModel { Id = 1, Name = "League One" } };
            var stadiums = new List<StadiumSimpleViewModel> { new StadiumSimpleViewModel { Id = 2, Name = "Stadium One" } };

            mockRepository.Setup(x => x.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync(team);
            mockLeagueService.Setup(x => x.GetAllLeaguesAsync()).ReturnsAsync(leagues);
            mockStadiumService.Setup(x => x.GetAllStadiumAsync()).ReturnsAsync(stadiums);

            // Act
            var result = await teamService.GetEditFormViewModel(teamId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamId, result.Id);
            Assert.Equal("Test Team", result.Name);
            Assert.Equal(leagues, result.Leagues);
            Assert.Equal(stadiums, result.Stadiums);
        }

        [Fact]
        public async Task DeleteTeamAsync_TeamHasMatches_ReturnsFailure()
        {
            // Arrange
            int teamId = 1;
            var team = new DataModels.Team { Id = teamId };

            var matches = new List<DataModels.Match> {
                new DataModels.Match { HomeTeamId = teamId 
                } 
            }.AsAsyncQueryable();

            mockRepository.Setup(x => x.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync(team);
            mockRepository.Setup(x => x.AllReadonly<DataModels.Match>()).Returns(matches);

            // Act
            var result = await teamService.DeleteTeamAsync(teamId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("You cannot delete a team with matches.", result.Message);
        }

        [Fact]
        public async Task DeleteTeamAsync_SuccessfulDeletion_ReturnsSuccess()
        {
            // Arrange
            int teamId = 1;
            var team = new DataModels.Team { Id = teamId };

            var players = new List<DataModels.Player> {}.AsAsyncQueryable();
            var matches = new List<DataModels.Match> {}.AsAsyncQueryable();

            mockRepository.Setup(x => x.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync(team);
            mockRepository.Setup(x => x.AllReadonly<DataModels.Player>()).Returns(players);
            mockRepository.Setup(x => x.AllReadonly<DataModels.Match>()).Returns(matches);
            mockRepository.Setup(x => x.RemoveAsync(team)).Returns(Task.CompletedTask);
            mockRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await teamService.DeleteTeamAsync(teamId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal($"Successfully deleted {team.Id}!", result.Message);
        }

        [Fact]
        public async Task UpdateTeamAsync_TeamExists_ReturnsSuccess()
        {
            // Arrange
            int teamId = 1;
            var team = new DataModels.Team
            {
                Id = teamId,
                Name = "Original Name",
                LogoURL = "original-url.png"
            };

            var viewModel = new TeamFormViewModel
            {
                Name = "Updated Name",
                LogoURL = "updated-url.png",
                StadiumId = 2,
                LeagueId = 3,
                Coach = "New Coach",
                Founded = 1999
            };

            mockRepository.Setup(x => x.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync(team);
            mockRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await teamService.UpdateTeamAsync(viewModel, teamId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Team edited successfully!", result.Message);
            Assert.Equal("Updated Name", team.Name);
            Assert.Equal("updated-url.png", team.LogoURL);
        }

        [Fact]
        public async Task UpdateTeamAsync_TeamNotFound_ReturnsFailure()
        {
            // Arrange
            int teamId = 1;
            var viewModel = new TeamFormViewModel
            {
                Name = "Any Name"
            };

            mockRepository.Setup(x => x.GetByIdAsync<DataModels.Team>(teamId)).ReturnsAsync((DataModels.Team)null);

            // Act
            var result = await teamService.UpdateTeamAsync(viewModel, teamId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Team not found!", result.Message);
        }
    }
}
