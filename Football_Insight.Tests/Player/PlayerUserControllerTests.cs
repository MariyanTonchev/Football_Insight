using Football_Insight.Areas.User.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Player;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Football_Insight.Tests.Player
{
    public class PlayerUserControllerTests
    {
        private readonly Mock<IPlayerService> mockPlayerService;
        private readonly Mock<ILogger<PlayerController>> mockLogger;
        private readonly PlayerController controller;

        public PlayerUserControllerTests()
        {
            mockPlayerService = new Mock<IPlayerService>();
            mockLogger = new Mock<ILogger<PlayerController>>();
            controller = new PlayerController(mockPlayerService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task Index_InvalidPlayerId_ReturnsBadRequest()
        {
            // Arrange
            int invalidPlayerId = 0;

            // Act
            var result = await controller.Index(invalidPlayerId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Index_PlayerNotFound_ReturnsNotFound()
        {
            // Arrange
            int validPlayerId = 1;
            mockPlayerService.Setup(service => service.GetPlayerDetailsAsync(validPlayerId)).ReturnsAsync((PlayerSquadViewModel)null);

            // Act
            var result = await controller.Index(validPlayerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Index_ValidPlayerId_ReturnsViewWithModel()
        {
            // Arrange
            int validPlayerId = 1;
            var viewModel = new PlayerSquadViewModel { Id = validPlayerId, FirstName = "Test", LastName = "Testov" };
            mockPlayerService.Setup(service => service.GetPlayerDetailsAsync(validPlayerId)).ReturnsAsync(viewModel);

            // Act
            var result = await controller.Index(validPlayerId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<PlayerSquadViewModel>(viewResult.Model);
            Assert.Equal(validPlayerId, model.Id);
        }

        [Fact]
        public async Task Index_ServiceThrowsException_ReturnsStatusCode500()
        {
            // Arrange
            int validPlayerId = 1;
            mockPlayerService.Setup(service => service.GetPlayerDetailsAsync(validPlayerId)).ThrowsAsync(new Exception());

            // Act
            var result = await controller.Index(validPlayerId);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
