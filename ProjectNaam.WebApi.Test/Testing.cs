using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectNaam.WebApi.Controllers;
using ProjectNaam.WebApi.Services;

namespace ProjectNaam.WebApi.Test
{
    [TestClass]
    public sealed class Testing
    {
        [TestMethod]
        public void TestCreateEnvironment()
        {
            var environmentRepository = new Mock<IEnvironmentService>();
            var logger = new Mock<ILogger<Environment2DController>>();
            var authRepository = new Mock<IAuthenticationService>();
            // Arrange

            //Fake User
            var userId = Guid.NewGuid().ToString();
            authRepository.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);

            //Fake Environment Controller + Environment
            var controller = new Environment2DController(environmentRepository.Object, logger.Object, authRepository.Object);
            var environment = new Environment2D
            {
                Id = Guid.NewGuid(),
                Name = "Test Environment",
                MaxHeight = 10,
                MaxLength = 10
            };
            // Act / Call
            var result = controller.Add(environment);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public void TestUpdateEnvironment()
        {
            var environmentRepository = new Mock<IEnvironmentService>();
            var logger = new Mock<ILogger<Environment2DController>>();
            var authRepository = new Mock<IAuthenticationService>();
            // Arrange

            //Fake User + Environment
            var userId = Guid.NewGuid().ToString();
            var environmentId = Guid.NewGuid();
            authRepository.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
            var environment = new Environment2D
            {
                Id = environmentId,
                Name = "Test Environment",
                MaxHeight = 10,
                MaxLength = 10
            };
            environmentRepository.Setup(x => x.ReadAsync(environmentId)).ReturnsAsync(environment);

            var controller = new Environment2DController(environmentRepository.Object, logger.Object, authRepository.Object);
            var environmentUpdate = new Environment2D
            {
                Id = environmentId,
                Name = "Test Environment2",
                MaxHeight = 100,
                MaxLength = 100
            };
            var result = controller.Update(environmentId, environmentUpdate);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void TestDeleteEnvironment()
        {
            var environmentRepository = new Mock<IEnvironmentService>();
            var logger = new Mock<ILogger<Environment2DController>>();
            var authRepository = new Mock<IAuthenticationService>();
            // Arrange

            //Fake User + Environment
            var userId = Guid.NewGuid().ToString();
            var enviromentId = Guid.NewGuid();
            authRepository.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
            environmentRepository.Setup(x => x.ReadAsync(enviromentId)).ReturnsAsync(new Environment2D());

            var controller = new Environment2DController(environmentRepository.Object, logger.Object, authRepository.Object);
            var result = controller.Update(enviromentId);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            // Check if the delete method is called
            environmentRepository.Verify(x => x.DeleteAsync(enviromentId), Times.Once);
        }
    }
}
