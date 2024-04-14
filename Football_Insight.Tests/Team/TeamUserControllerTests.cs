using Football_Insight.Areas.User.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Football_Insight.Tests.Team
{
    public class TeamUserControllerTests
    {
        private readonly Mock<ITeamService> mockTeamService;
        private readonly Mock<ILeagueService> mockLeagueService;
        private readonly Mock<IStadiumService> mockStadiumService;
        private readonly Mock<ILogger<TeamController>> mockLogger;
        private readonly TeamController controller;

        public TeamUserControllerTests()
        {
            mockTeamService = new Mock<ITeamService>();
            mockLeagueService = new Mock<ILeagueService>();
            mockStadiumService = new Mock<IStadiumService>();
            mockLogger = new Mock<ILogger<TeamController>>();
            controller = new TeamController(mockLeagueService.Object, mockTeamService.Object, mockStadiumService.Object, mockLogger.Object);
        }

        [Fact]
        public void Index_ValidTeamId_ReturnsViewWithTeamId()
        {
            // Arrange
            int validTeamId = 1;

            // Act
            var result = controller.Index(validTeamId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(validTeamId, viewResult.Model);
        }

        [Fact]
        public void Index_InvalidTeamId_ReturnsBadRequest()
        {
            // Arrange
            int invalidTeamId = 0;

            // Act
            var result = controller.Index(invalidTeamId);

            // Assert
            Assert.IsType<BadRequestResult>(result); 
        }

        [Fact]
        public async Task All_ReturnsViewWithModel_WhenDataExists()
        {
            // Arrange
            var expectedViewModel = new List<LeagueTeamsViewModel> { new LeagueTeamsViewModel() { Id = 1, Name = "Premier League" } };

            mockLeagueService.Setup(s => s.GetAllLeaguesWithTeamsAsync()).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.All();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<LeagueTeamsViewModel>>(viewResult.Model);
            Assert.Equal(expectedViewModel, model);
        }

        [Fact]
        public async Task All_ReturnsNotFound_WhenDataIsAbsent()
        {
            // Arrange
            mockLeagueService.Setup(s => s.GetAllLeaguesWithTeamsAsync()).ReturnsAsync((List<LeagueTeamsViewModel>)null);

            // Act
            var result = await controller.All();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task All_ReturnsStatusCode500_OnException()
        {
            // Arrange
            var exception = new Exception("Test exception");

            mockLeagueService.Setup(s => s.GetAllLeaguesWithTeamsAsync()).ThrowsAsync(exception);

            // Act
            var result = await controller.All();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Fixtures_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int teamId = 0;

            // Act
            var result = await controller.Fixtures(teamId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Fixtures_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            int validId = 1;
            var expectedViewModel = new TeamFixturesViewModel();

            mockTeamService.Setup(s => s.GetTeamFixturesAsync(validId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Fixtures(validId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewModel, viewResult.Model);
        }

        [Fact]
        public async Task Fixtures_NoData_ReturnsNotFound()
        {
            // Arrange
            int validId = 1;

            mockTeamService.Setup(s => s.GetTeamFixturesAsync(validId)).ReturnsAsync((TeamFixturesViewModel)null);

            // Act
            var result = await controller.Fixtures(validId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Fixtures_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            int validId = 1;
            var exception = new Exception("Test exception");

            mockTeamService.Setup(s => s.GetTeamFixturesAsync(validId)).ThrowsAsync(exception);

            // Act
            var result = await controller.Fixtures(validId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Results_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            int validId = 1;
            var expectedViewModel = new TeamResultsViewModel();

            mockTeamService.Setup(s => s.GetTeamResultsAsync(validId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Results(validId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewModel, viewResult.Model);
        }

        [Fact]
        public async Task Results_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int teamId = 0;

            // Act
            var result = await controller.Results(teamId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Results_NoData_ReturnsNotFound()
        {
            // Arrange
            int validId = 1;

            mockTeamService.Setup(s => s.GetTeamResultsAsync(validId)).ReturnsAsync((TeamResultsViewModel)null);

            // Act
            var result = await controller.Results(validId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Results_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            int validId = 1;
            var exception = new Exception("Test exception");

            mockTeamService.Setup(s => s.GetTeamResultsAsync(validId)).ThrowsAsync(exception);

            // Act
            var result = await controller.Results(validId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Squad_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int teamId = 0;

            // Act
            var result = await controller.Squad(teamId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Squad_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            int validId = 1;
            var expectedViewModel = new TeamSquadViewModel();

            mockTeamService.Setup(s => s.GetTeamSquadAsync(validId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Squad(validId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewModel, viewResult.Model);
        }

        [Fact]
        public async Task Squad_NoData_ReturnsNotFound()
        {
            // Arrange
            int validId = 1;

            mockTeamService.Setup(s => s.GetTeamSquadAsync(validId)).ReturnsAsync((TeamSquadViewModel)null);

            // Act
            var result = await controller.Squad(validId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Squad_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            int validId = 1;
            var exception = new Exception("Test exception");

            mockTeamService.Setup(s => s.GetTeamSquadAsync(validId)).ThrowsAsync(exception);

            // Act
            var result = await controller.Squad(validId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
