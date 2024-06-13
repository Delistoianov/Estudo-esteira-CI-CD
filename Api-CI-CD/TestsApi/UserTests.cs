using Api_CI_CD.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.Linq;
using static Api_CI_CD.Controllers.UsersController;

namespace TestsApi
{
    public class UnitTest1
    {
        private readonly UsersController _controller;
        private readonly List<User> _users;

        public UnitTest1()
        {
            // Mock initial user data
            _users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new User { Id = 2, Name = "Bob", Email = "bob@example.com" },
                new User { Id = 3, Name = "Charlie", Email = "charlie@example.com" }
            };
            _controller = new UsersController();
        }

/*        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var items = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }*/

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFound()
        {
            // Act
            var result = _controller.Get(99);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var newUser = new User { Id = 4, Name = "Dave", Email = "dave@example.com" };

            // Act
            var result = _controller.Post(newUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var item = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal("Dave", item.Name);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Act
            var result = _controller.Delete(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasUpdatedItem()
        {
            // Arrange
            var testUser = new User { Id = 1, Name = "Updated Alice", Email = "updatedalice@example.com" };

            // Act
            var result = _controller.Put(1, testUser);

            // Assert
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var updatedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("Updated Alice", updatedUser.Name);
        }
    }
}
