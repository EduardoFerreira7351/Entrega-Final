using System;
using System.Threading.Tasks;
using FastPoint.Application.Interfaces;
using FastPoint.Application.Services;
using FastPoint.Domain.Entities;
using Moq;
using Xunit;

namespace FastPoint.Tests.Unit
{
    public class UserServiceTests
    {
        [Fact]
        public async Task AddUser_CallsRepository()
        {
            var userRepo = new Mock<IUserRepository>();
            var service = new UserService(userRepo.Object);

            var user = new User { Id = Guid.NewGuid(), Email = "t@t.com", FullName = "T", PasswordHash = "h" };

            await service.AddAsync(user);

            userRepo.Verify(r => r.AddAsync(It.Is<User>(u => u == user)), Times.Once);
            userRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_Throws_WhenNotFound()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);
            var service = new UserService(userRepo.Object);

            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteAsync(Guid.NewGuid()));
        }
    }
}
