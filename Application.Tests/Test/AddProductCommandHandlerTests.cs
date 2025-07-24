using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.CommandHandler.ProductCommandHandler;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Test
{
    public class AddProductCommandHandlerTests
    {
        [Fact]
        public async Task successful()
        {
            var command = new AddProductCommand
            {
                Name = "K52",
                ProduceDate = DateTime.Parse("2025-07-23T15:20:30.061"),
                ManufacturePhone = "09173112113",
                ManufactureEmail = "agsm@gmail.com",
                IsAvailable = true,
                CreatedByUserId = "user123"

            };

            var fakeProduct = new Product(
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable,
                command.CreatedByUserId
            );

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Product>(It.IsAny<AddProductCommand>()))
                      .Returns(fakeProduct);

            var repoMock = new Mock<IGenericRepository<Product>>();
            repoMock.Setup(r => r.Add(It.IsAny<Product>())).ReturnsAsync(fakeProduct.Id.ToString());
            repoMock.Setup(r => r.SaveChange()).Returns(Task.CompletedTask);
            var handler = new AddProductCommandHandler(repoMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(fakeProduct.Id.ToString(), result);
        }
        [Fact]
        public async Task UnSuccessfull()
        {
            var command = new AddProductCommand
            {
                Name = "K52",
                ProduceDate = DateTime.Parse("2025-07-23T15:20:30.061"),
                ManufacturePhone = "09173112113",
                ManufactureEmail = "agsm@gmail.com",
                IsAvailable = true,
                CreatedByUserId = "user123"

            };

            var fakeProduct = new Product(
                command.Name,
                command.ProduceDate,
                command.ManufacturePhone,
                command.ManufactureEmail,
                command.IsAvailable,
                command.CreatedByUserId
            );

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Product>(It.IsAny<AddProductCommand>()))
          .Returns(fakeProduct);
            var repoMock = new Mock<IGenericRepository<Product>>();
            repoMock.Setup(r => r.Add(It.IsAny<Product>())).ThrowsAsync(new Exception("خطا در اضافه کردن محصول"));
            repoMock.Setup(r => r.SaveChange()).Returns(Task.CompletedTask);
            var handler = new AddProductCommandHandler(repoMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotEqual(fakeProduct.Id.ToString(), result);
            Assert.Equal("0", result);

        }
    }
}
