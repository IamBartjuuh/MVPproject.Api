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
            Assert.IsTrue(result.Result is OkResult);
        }

        [TestMethod]
        public void TestUpdateEnvironment()
        {
            var environmentRepository = new Mock<IEnvironmentService>();
            var logger = new Mock<ILogger<Environment2DController>>();
            var authRepository = new Mock<IAuthenticationService>();
            // Arrange

            //Fake User
            var userId = Guid.NewGuid().ToString();
            authRepository.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);

            var controller = new Environment2DController(environmentRepository.Object, logger.Object, authRepository.Object);
            var environment = new Environment2D
            {
                Id = Guid.NewGuid(),
                Name = "Test Environment",
                MaxHeight = 10,
                MaxLength = 10
            };
            // Act
            controller.Add(environment);
            var environmentUpdate = new Environment2D
            {
                Id = environment.Id,
                Name = "Test Environment2",
                MaxHeight = 100,
                MaxLength = 100
            };
            var result = controller.Update(environment.Id, environmentUpdate);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteEnvironment()
        {
            var environmentRepository = new Mock<IEnvironmentService>();
            var logger = new Mock<ILogger<Environment2DController>>();
            var authRepository = new Mock<IAuthenticationService>();
            // Arrange

            //Fake User
            var userId = Guid.NewGuid().ToString();
            authRepository.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);

            var controller = new Environment2DController(environmentRepository.Object, logger.Object, authRepository.Object);
            var environment = new Environment2D
            {
                Id = Guid.NewGuid(),
                Name = "Test Environment",
                MaxHeight = 10,
                MaxLength = 10
            };
            // Act
            controller.Add(environment);
            var result = controller.Update(environment.Id);
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
