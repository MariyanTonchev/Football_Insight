using Football_Insight.Areas.Admin.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Player;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Football_Insight.Tests.Player
{
    public class PlayerAdminControllerTests
    {
        private readonly Mock<IPlayerService> mockPlayerService;
        private readonly Mock<ILogger<PlayerController>> mockLogger;
        private readonly PlayerController controller;

        public PlayerAdminControllerTests()
        {
            mockPlayerService = new Mock<IPlayerService>();
            mockLogger = new Mock<ILogger<PlayerController>>();
            controller = new PlayerController(mockPlayerService.Object, mockLogger.Object);
        }

        [Fact]
        public void Create_Get_ValidTeamId_ReturnsViewWithModel()
        {
            // Arrange
            var teamId = 1;
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(x => x.GetCreateFomViewModel(teamId)).Returns(viewModel);

            // Act
            var result = controller.Create(teamId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public void Create_Get_InvalidTeamId_ReturnsNotFound()
        {
            // Arrange
            mockPlayerService.Setup(x => x.GetCreateFomViewModel(It.IsAny<int>())).Returns((PlayerFormViewModel)null);

            // Act
            var result = controller.Create(99999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ThrowsException_ReturnsStatusCode500()
        {
            // Arrange
            var teamId = 1;
            mockPlayerService.Setup(x => x.GetCreateFomViewModel(teamId)).Throws(new Exception("Test exception"));

            // Act
            var result = controller.Create(teamId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_Post_ValidModel_CreatesAndRedirects()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel { TeamId = 1 };
            mockPlayerService.Setup(x => x.CreatePlayerAsync(It.IsAny<PlayerFormViewModel>()))
                             .ReturnsAsync(new OperationResult(true, ""));

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Squad", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
            Assert.Equal("User", redirectToActionResult.RouteValues["Area"]);
            Assert.Equal(viewModel.TeamId, redirectToActionResult.RouteValues["Id"]);
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            controller.ModelState.AddModelError("Error", "Sample error");
            var viewModel = new PlayerFormViewModel();

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public async Task Create_Post_Failure_ReturnsViewWithError()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(x => x.CreatePlayerAsync(It.IsAny<PlayerFormViewModel>()))
                             .ReturnsAsync(new OperationResult(false, "Creation failed"));

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("Creation failed", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Create_Post_ThrowsException_ReturnsViewWithModelError()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(x => x.CreatePlayerAsync(It.IsAny<PlayerFormViewModel>()))
                             .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("An unexpected error occurred while creating the player.", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Delete_Get_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await controller.Delete(-1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_Get_NoPlayerFound_ReturnsNotFound()
        {
            // Arrange
            mockPlayerService.Setup(service => service.GetPlayerSimpleViewModelAsync(It.IsAny<int>()))
                             .ReturnsAsync((PlayerSimpleViewModel)null);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ValidRequest_ReturnsViewWithModel()
        {
            // Arrange
            var viewModel = new PlayerSimpleViewModel { PlayerId = 1 };
            mockPlayerService.Setup(service => service.GetPlayerSimpleViewModelAsync(1))
                             .ReturnsAsync(viewModel);

            // Act
            var result = await controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public async Task Delete_Get_ThrowsException_ReturnsStatusCode500()
        {
            // Arrange
            mockPlayerService.Setup(service => service.GetPlayerSimpleViewModelAsync(It.IsAny<int>()))
                             .ThrowsAsync(new Exception());

            // Act
            var result = await controller.Delete(1);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Post_SuccessfulDeletion_RedirectsToSquad()
        {
            // Arrange
            var viewModel = new PlayerSimpleViewModel { PlayerId = 1, TeamId = 1 };
            mockPlayerService.Setup(service => service.DeletePlayerAsync(1))
                             .ReturnsAsync(new OperationResult(true, ""));

            // Act
            var result = await controller.Delete(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Squad", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Delete_Post_FailedDeletion_ReturnsViewWithError()
        {
            // Arrange
            var viewModel = new PlayerSimpleViewModel { PlayerId = 1 };
            mockPlayerService.Setup(service => service.DeletePlayerAsync(1))
                             .ReturnsAsync(new OperationResult(false, "Deletion failed"));

            // Act
            var result = await controller.Delete(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("Deletion failed", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Delete_Post_ThrowsException_ReturnsViewWithError()
        {
            // Arrange
            var viewModel = new PlayerSimpleViewModel { PlayerId = 1 };
            mockPlayerService.Setup(service => service.DeletePlayerAsync(1))
                             .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.Delete(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("An unexpected error occurred while trying to delete the player.", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Edit_Get_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await controller.Edit(-1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ValidId_ReturnsViewWithModel()
        {
            // Arrange
            var playerId = 1;
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(service => service.GetEditFormViewModel(playerId)).ReturnsAsync(viewModel);

            // Act
            var result = await controller.Edit(playerId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Get_PlayerNotFound_ReturnsNotFound()
        {
            // Arrange
            var playerId = 1;
            mockPlayerService.Setup(service => service.GetEditFormViewModel(playerId)).ReturnsAsync((PlayerFormViewModel)null);

            // Act
            var result = await controller.Edit(playerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ThrowsException_ReturnsStatusCode500()
        {
            // Arrange
            var playerId = 1;
            mockPlayerService.Setup(service => service.GetEditFormViewModel(playerId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.Edit(playerId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Edit_Post_NullViewModel_ReturnsBadRequest()
        {
            // Act
            var result = await controller.Edit(null, 1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_Post_InvalidModelState_ReturnsViewWithOriginalModel()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel();
            controller.ModelState.AddModelError("Error", "Sample error");

            // Act
            var result = await controller.Edit(viewModel, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewModel, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Post_SuccessfulUpdate_RedirectsToSquad()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel { TeamId = 1 };
            mockPlayerService.Setup(service => service.UpdatePlayerAsync(It.IsAny<PlayerFormViewModel>(), It.IsAny<int>()))
                             .ReturnsAsync(new OperationResult(true, ""));

            // Act
            var result = await controller.Edit(viewModel, 1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Squad", redirectToActionResult.ActionName);
            Assert.Equal("Team", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Edit_Post_UpdateFailure_ReturnsViewWithError()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(service => service.UpdatePlayerAsync(It.IsAny<PlayerFormViewModel>(), It.IsAny<int>()))
                             .ReturnsAsync(new OperationResult(false, "Update failed"));

            // Act
            var result = await controller.Edit(viewModel, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("Update failed", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Edit_Post_ThrowsException_ReturnsViewWithError()
        {
            // Arrange
            var viewModel = new PlayerFormViewModel();
            mockPlayerService.Setup(service => service.UpdatePlayerAsync(It.IsAny<PlayerFormViewModel>(), It.IsAny<int>()))
                             .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.Edit(viewModel, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Contains("An unexpected error occurred while trying to update the player.", viewResult.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }
    }
}
