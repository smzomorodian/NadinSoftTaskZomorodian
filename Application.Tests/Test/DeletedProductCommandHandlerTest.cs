using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.CommandHandler.ProductCommandHandler;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Test
{
    public class DeletedProductCommandHandlerTest
    {
        [Fact]
        public async Task Successful()
        {
            var command = new DeletedProductCommand
            {
                ProductId = Guid.NewGuid()
            };

            var fakeProduct = new Product(
                "K52",
                DateTime.Parse("2025-07-23T15:20:30.061"),
                "09173112113",
                "agsm@gmail.com",
                true,
                "user123"
            );
            fakeProduct.SetId(command.ProductId);

            var getRepoMock = new Mock<IProductReadonlyRepository>();
            getRepoMock.Setup(r => r.GetById(command.ProductId)).ReturnsAsync(fakeProduct);

            var crudRepoMock = new Mock<IGenericRepository<Product>>();
            crudRepoMock.Setup(r => r.SaveChange()).Returns(Task.CompletedTask);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user123")
            }, "mock"));
            var httpContext = new DefaultHttpContext { User = user };
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var handler = new DeletedProductCommandHandler(crudRepoMock.Object, httpContextAccessorMock.Object, getRepoMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Unsuccessful()
        {
            var command = new DeletedProductCommand
            {
                ProductId = Guid.NewGuid()
            };

            var fakeProduct = new Product(
                "K52",
                DateTime.Parse("2025-07-23T15:20:30.061"),
                "09173112113",
                "agsm@gmail.com",
                true,
                "user123"
            );
            fakeProduct.SetId(command.ProductId);
            var getRepoMock = new Mock<IProductReadonlyRepository>();
            getRepoMock.Setup(r => r.GetById(command.ProductId)).ReturnsAsync((Product?)null);
            var crudRepoMock = new Mock<IGenericRepository<Product>>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                 new Claim(ClaimTypes.NameIdentifier, "user123")
                        }, "mock"));
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext { User = user });
            var handler = new DeletedProductCommandHandler(crudRepoMock.Object, httpContextAccessorMock.Object, getRepoMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.False(result);

        }

    }
}
