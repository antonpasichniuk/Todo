using Moq;
using System.Linq.Expressions;
using Todo.Application.Models.Requests.TasksList;
using Todo.Application.Repositories.Interfaces;
using Todo.Application.Services;
using Todo.Application.Services.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Tests.Services
{
    public class TasksListServiceTests
    {
        private readonly Mock<ITasksListRepository> _repositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IUserContext> _userContextMock = new();
        private readonly TasksListService _service;

        public TasksListServiceTests()
        {
            _service = new TasksListService(_userContextMock.Object, _repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task CreateTasksListAsync_ShouldReturnError_WhenCreationFails()
        {
            // Arrange
            var request = new CreateTasksList { Name = string.Empty };

            // Act
            var result = await _service.CreateTasksListAsync(request);

            // Assert
            Assert.True(result.IsFailure);
            _repositoryMock.Verify(x => x.Add(It.IsAny<TasksList>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
