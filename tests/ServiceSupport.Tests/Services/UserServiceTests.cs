using System.Threading.Tasks;
using ServiceSupport.Infrastructure.Services;
using Xunit;
using ServiceSupport.Core.Repositories;
using AutoMapper;
using ServiceSupport.Core.Domain;
using System;
using ServiceSupport.Infrastructure.Services.UserGroup;
using Moq;
using System.Collections.Generic;

namespace ServiceSupport.Tests
{
    public class UserServiceTests
    {

        [Fact]
        public async Task Register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(), "user@email.com", "user1", "secret", "user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task When_calling_get_async_and_user_exists_it_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.GetAsync("user1@email.com");

            var user = new User(Guid.NewGuid(), "user1@email.com", "user1", "user", "secret", "salt");

            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                              .ReturnsAsync(user);

            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_user_does_not_exist_it_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.GetAsync("user@email.com");

            userRepositoryMock.Setup(x => x.GetAsync("user@email.com"))
                              .ReturnsAsync(() => null);

            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_users_exists_it_should_invoke_users_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("hash");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("salt");
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.BrowseAsync();

            var users = new HashSet<User>()
            {
                new User(Guid.NewGuid(), "user1@email.com", "user1", "user", "secret", "salt"),
                new User(Guid.NewGuid(), "user2@email.com", "user2", "user", "secret", "salt"),
                new User(Guid.NewGuid(), "user3@email.com", "user3", "user", "secret", "salt")
            };

            userRepositoryMock.Setup(x => x.GetAllAsync())
                              .ReturnsAsync(users);

            userRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }
    }
}