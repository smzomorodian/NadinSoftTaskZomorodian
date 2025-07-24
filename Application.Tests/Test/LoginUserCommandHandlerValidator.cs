//using Application.NadinSoft.Command.User;
//using Domain.NadinSoft.Model;
//using MapsterMapper;
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
//    public class LoginUserCommandHandlerValidator
//    {
//        [Fact]
//        public async Task successful()
//        {
//            var command = new LoginUserCommand
//            {
//                Password ="123ASas456",
//                NationalCode = "2253316778"
//            };

//            var fakeUser = new ApplicationUser("2253316778")
//            {
//                Id = "1",
//                Email = "test@example.com",
//            };
//            var mapperMock = new Mock<IMapper>();
//            mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<RegisterUserCommand>())).Returns(fakeUser);

//        }
//    }
//}
