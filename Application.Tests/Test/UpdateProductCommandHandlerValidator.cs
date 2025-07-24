using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.CommandHandler.ProductCommandHandler;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Test
{
    public class UpdateProductCommandHandlerValidator
    {
        [Fact]
        public async Task UpdateProduct_Success()
        {
            var command = new UpdateProductCommand
            {
                ProductId = Guid.NewGuid(),
                Name = "NewName",
                ProduceDate = DateTime.UtcNow,
                ManufacturePhone = "09173115445",
                ManufactureEmail = "new@gmail.com",
                IsAvailable = true
            };

            var product = new Product("OldName", DateTime.UtcNow, "0917000000", "old@gmail.com", false, "user123");
            product.SetId(command.ProductId);

            var getRepoMock = new Mock<IProductReadonlyRepository>();
            getRepoMock.Setup(r => r.GetById(command.ProductId)).ReturnsAsync(product);

            var crudRepoMock = new Mock<IGenericRepository<Product>>();
            crudRepoMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(true);
            crudRepoMock.Setup(r => r.SaveChange()).Returns(Task.CompletedTask);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "user123") }));
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext { User = user });

            var handler = new UpdateProductCommandHandler(crudRepoMock.Object, null!, httpContextAccessorMock.Object, getRepoMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("ویرایش با موفقیت انجام شد", result);
        }
    }
}
