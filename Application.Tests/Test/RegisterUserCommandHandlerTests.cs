using Application.NadinSoft.Command.User;
using Application.NadinSoft.CommandHandler.UserCommandHandler;
using Domain.NadinSoft.Model;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task successful()
        {
            var command = new RegisterUserCommand { Email = "test@test.com", Password = "Test123!" };
            var fakeUser = new ApplicationUser("2253316778")
            {
                Id = "1",
                Email = "test@example.com",
            };
            ;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<RegisterUserCommand>())).Returns(fakeUser);

            var userStore = new Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                           .ReturnsAsync(Microsoft.AspNetCore.Identity.IdentityResult.Success);

            var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("1", result);
        }

        [Fact]
        public async Task Unsuccessful()
        {
            var command = new RegisterUserCommand { Email = "fail@test.com", Password = "Test123!" };
            var fakeUser = new ApplicationUser("2253316778")
            {
                Id = "1",
                Email = "test@example.com",
            };


            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<RegisterUserCommand>())).Returns(fakeUser);

            var userStore = new Mock<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                           .ReturnsAsync(Microsoft.AspNetCore.Identity.IdentityResult.Failed(new IdentityError { Description = "خطا در ثبت نام" }));

            var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("خطا در ثبت نام", result);
        }
    }
}
