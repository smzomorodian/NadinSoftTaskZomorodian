//using Application.NadinSoft.Command.User;
//using Application.NadinSoft.CommandHandler.UserCommandHandler;
//using Castle.Core.Configuration;
//using Domain.NadinSoft.Interface;
//using Domain.NadinSoft.Model;
//using MapsterMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Application.Tests.Test
//{
//    public class LoginUserCommandHandlerTest
//    {
//        private Mock<SignInManager<ApplicationUser>> GetSignInManagerMock()
//        {
//            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
//            return new Mock<SignInManager<ApplicationUser>>(
//                new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null).Object,
//                new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>().Object,
//                new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>().Object,
//                new Mock<Microsoft.Extensions.Logging.ILogger<SignInManager<ApplicationUser>>>().Object,
//                new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>().Object,
//                null
//            );
//        }

//        private Microsoft.Extensions.Configuration.IConfiguration GetConfig()
//        {
//            var inMemorySettings = new Dictionary<string, string>
//            {
//                {"Jwt:Key", "secret_key_123456789"},
//                {"Jwt:Issuer", "my_issuer"},
//                {"Jwt:Audience", "my_audience"}
//            };

//            return new ConfigurationBuilder()
//                .AddInMemoryCollection(inMemorySettings)
//                .Build();
//        }

//        [Fact]
//        public async Task Login_Success()
//        {
//            var command = new LoginUserCommand
//            {
//                Password = "123ASas456",
//                NationalCode = "2253316778"
//            };

//            var fakeUser = new ApplicationUser("2253316778")
//            {
//                Id = "1",
//                Email = "test@example.com"
//            };

//            var userRepoMock = new Mock<IUserReadonlyRepository>();
//            userRepoMock.Setup(r => r.GetByNationalCodeAsync(command.NationalCode)).ReturnsAsync(fakeUser);

//            var mapperMock = new Mock<IMapper>();
//            var signInManagerMock = GetSignInManagerMock();
//            var config = GetConfig();

//            var handler = new LoginUserCommandHandler(
//                userRepoMock.Object,
//                mapperMock.Object,
//                signInManagerMock.Object,
//                config);

//            var result = await handler.Handle(command, CancellationToken.None);

//            Assert.False(string.IsNullOrEmpty(result));
//            Assert.DoesNotContain("کاربر", result);
//        }

//        [Fact]
//        public async Task Login_Failed_UserNotFound()
//        {
//            var command = new LoginUserCommand
//            {
//                Password = "anyPassword",
//                NationalCode = "9999999999"
//            };

//            var userRepoMock = new Mock<IUserReadonlyRepository>();
//            userRepoMock.Setup(r => r.GetByNationalCodeAsync(command.NationalCode)).ReturnsAsync((ApplicationUser?)null);

//            var mapperMock = new Mock<IMapper>();
//            var signInManagerMock = GetSignInManagerMock();
//            var config = GetConfig();

//            var handler = new LoginUserCommandHandler(
//                userRepoMock.Object,
//                mapperMock.Object,
//                signInManagerMock.Object,
//                config);

//            var result = await handler.Handle(command, CancellationToken.None);

//            Assert.Equal("کاربر یافت نشد", result);
//        }
//    }
//}
