using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using VendingMachine.Exceptions;
using VendingMachine.Models.Entities;
using VendingMachine.Repositories;
using VendingMachine.Services.Impl;

namespace VendingMachineTest.Services
{
    public class SnackServiceTest
    {

        /* A thing to do:
            var mock = new Mock<IFoo>();
            mock.SetupSequence(f => f.GetCount())
                .Returns(3)  // will be returned on 1st invocation
                .Returns(2)  // will be returned on 2nd invocation
                .Returns(1)  // will be returned on 3rd invocation
                .Returns(0)  // will be returned on 4th invocation
                .Throws(new InvalidOperationException());
         */

        private readonly Mock<ISnackRepository> _mockSnackRepository = new Mock<ISnackRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly SnackService _snackService;

        public SnackServiceTest()
        {
            _snackService = new (_mockSnackRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async void FindByIdOrThrowAsync_ReturnSnack()
        {
            // Arrange
            long id = 1;
            Snack snack = new Snack()
            {
                Id = id,
                Name = "Snacky Poo",
                Cost = 1,
                Quantity = 1,

            };
            _mockSnackRepository.Setup(x => x.GetSnackByIdAsync(id)).ReturnsAsync(snack); 

            // Act
            Snack actual = await _snackService.FindByIdOrThrowAsync(id);

            // Assert
            Assert.Equal(snack.Id, actual.Id);
        }

        [Fact]
        public async void FindByIdOrThrowAsync_Throw()
        {
            // Arrange
            long id = 1;
            _mockSnackRepository.Setup(x => x.GetSnackByIdAsync(id)).ReturnsAsync(() => null);

            // Act
            Func<Task> act = async () => await _snackService.FindByIdOrThrowAsync(id);

            // Assert
            SnackNotFoundException exception = await Assert.ThrowsAsync<SnackNotFoundException>(act);
            Assert.Equal($"Snack with id {id} not found", exception.Message);
        }
    }
}