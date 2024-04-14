using Football_Insight.Areas.Admin.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;
using Football_Insight.Core.Models.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Football_Insight.Tests.Team
{
    public class TeamAdminControllerTests
    {
        private readonly Mock<ITeamService> mockTeamService;
        private readonly Mock<ILeagueService> mockLeagueService;
        private readonly Mock<IStadiumService> mockStadiumService;
        private readonly Mock<ILogger<TeamController>> mockLogger;
        private readonly TeamController controller;

        public TeamAdminControllerTests()
        {
            mockTeamService = new Mock<ITeamService>();
            mockLeagueService = new Mock<ILeagueService>();
            mockStadiumService = new Mock<IStadiumService>();
            mockLogger = new Mock<ILogger<TeamController>>();
            controller = new TeamController(mockLeagueService.Object, mockTeamService.Object, mockStadiumService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task Create_Get_ReturnsViewWithModel()
        {
            var expectedViewModel = new TeamFormViewModel();

            mockTeamService.Setup(service => service.GetCreateFormViewModel())
                           .ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TeamFormViewModel>(viewResult.Model);
            Assert.Equal(expectedViewModel, model);
        }

        [Fact]
        public async Task Create_Get_ThrowsException_ReturnsStatusCode500()
        {
            mockTeamService.Setup(service => service.GetCreateFormViewModel())
                           .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.Create();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_Post_Successful_ReturnsRedirectToAction()
        {
            // Arrange
            var viewModel = new TeamFormViewModel();

            mockTeamService.Setup(service => service.CreateTeamAsync(viewModel))
                           .ReturnsAsync(new OperationResult(true, "Created Successfully", 1));

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
            Assert.Equal("User", redirectToActionResult.RouteValues["Area"]);
            Assert.Equal(1, redirectToActionResult.RouteValues["TeamId"]);
        }

        [Fact]
        public async Task Create_Post_Failure_ReturnsViewWithModel()
        {
            // Arrange
            var viewModel = new TeamFormViewModel();

            mockTeamService.Setup(service => service.CreateTeamAsync(viewModel))
                           .ReturnsAsync(new OperationResult(false, "Creation Failed"));

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedViewModel = Assert.IsType<TeamFormViewModel>(viewResult.Model);
            Assert.Equal(viewModel, returnedViewModel);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.True(viewResult.ViewData.ModelState[""].Errors.Count > 0);
            Assert.Equal("Creation Failed", viewResult.ViewData.ModelState[""].Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task Edit_Get_ValidTeamId_ReturnsViewWithModel()
        {
            // Arrange
            int teamId = 1;
            var expectedViewModel = new TeamFormViewModel();

            mockTeamService.Setup(x => x.GetEditFormViewModel(teamId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Edit(teamId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewModel, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Get_InvalidTeamId_ReturnsNotFound()
        {
            // Arrange
            int teamId = 99;

            mockTeamService.Setup(x => x.GetEditFormViewModel(teamId)).ReturnsAsync((TeamFormViewModel)null);

            // Act
            var result = await controller.Edit(teamId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var viewModel = new TeamFormViewModel();
            int teamId = 1;

            mockTeamService.Setup(x => x.UpdateTeamAsync(viewModel, teamId)).ReturnsAsync(new OperationResult(true, "Success"));

            // Act
            var result = await controller.Edit(viewModel, teamId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
            Assert.Equal("User", redirectToActionResult.RouteValues["Area"]);
            Assert.Equal(teamId, redirectToActionResult.RouteValues["teamId"]);
        }

        [Fact]
        public async Task Edit_Post_InvalidData_ReturnsViewWithModel()
        {
            // Arrange
            var viewModel = new TeamFormViewModel();
            int teamId = 1;

            mockTeamService.Setup(x => x.UpdateTeamAsync(viewModel, teamId)).ReturnsAsync(new OperationResult(false, "Failure"));
            mockLeagueService.Setup(x => x.GetAllLeaguesAsync()).ReturnsAsync(new List<LeagueSimpleViewModel>());
            mockStadiumService.Setup(x => x.GetAllStadiumAsync()).ReturnsAsync(new List<StadiumSimpleViewModel>());

            // Act
            var result = await controller.Edit(viewModel, teamId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.True(viewResult.ViewData.ModelState.ErrorCount > 0);
        }

        [Fact]
        public async Task Delete_Get_ValidTeamId_ReturnsViewWithModel()
        {
            // Arrange
            int teamId = 1;
            var expectedViewModel = new TeamSimpleViewModel { TeamId = teamId, Name = "Sample Team" };

            mockTeamService.Setup(x => x.GetTeamSimpleViewModelAsync(teamId)).ReturnsAsync(expectedViewModel);

            // Act
            var result = await controller.Delete(teamId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewModel, viewResult.Model);
        }

        [Fact]
        public async Task Delete_Get_InvalidTeamId_ReturnsNotFound()
        {
            // Arrange
            int teamId = 99;

            mockTeamService.Setup(x => x.GetTeamSimpleViewModelAsync(teamId)).ReturnsAsync((TeamSimpleViewModel)null);

            // Act
            var result = await controller.Delete(teamId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Post_ValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var viewModel = new TeamSimpleViewModel { TeamId = 1, Name = "Sample Team" };

            mockTeamService.Setup(x => x.DeleteTeamAsync(viewModel.TeamId)).ReturnsAsync(new OperationResult(true, "Deleted Successfully"));

            // Act
            var result = await controller.Delete(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("All", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
            Assert.Equal("User", redirectToActionResult.RouteValues["Area"]);
        }

        [Fact]
        public async Task Delete_Post_Failure_ReturnsViewWithModel()
        {
            // Arrange
            var viewModel = new TeamSimpleViewModel { TeamId = 1, Name = "Sample Team" };

            mockTeamService.Setup(x => x.DeleteTeamAsync(viewModel.TeamId)).ReturnsAsync(new OperationResult(false, "Deletion Failed"));

            // Act
            var result = await controller.Delete(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.True(viewResult.ViewData.ModelState.ErrorCount > 0);
            Assert.Equal("Deletion Failed", viewResult.ViewData.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}
