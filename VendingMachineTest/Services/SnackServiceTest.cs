using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using VendingMachine.Exceptions;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Models.Entities;
using VendingMachine.Repositories;
using VendingMachine.Services.Impl;

namespace VendingMachineTest.Services
{
    public class SnackServiceTest
    {

        /*  A thing to do:
            var mock = new Mock<IFoo>();
            mock.SetupSequence(f => f.GetCount())
                .Returns(3)  // will be returned on 1st invocation
                .Returns(2)  // will be returned on 2nd invocation
                .Returns(1)  // will be returned on 3rd invocation
                .Returns(0)  // will be returned on 4th invocation
                .Throws(new InvalidOperationException());
         */

        // I think CallBase invokes actual method

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

        [Fact]
        public async void GetSnackByIdAsync()
        {
            // Arrange
            long id = 1;
            Snack snack = new ()
            {
                Id = id,
                Name = "Snacky Poo",
                Cost = 1,
                Quantity = 1
            };

            SnackResponseDto expected = new ()
            {
                Id = id,
                Name = "Snacky Poo",
                Cost = 1,
                Quantity = 1
            };

            Mock<SnackService> mockService = new (_mockSnackRepository.Object, _mockMapper.Object);
            mockService.Setup(x => x.FindByIdOrThrowAsync(id)).ReturnsAsync(snack);
            _mockMapper.Setup(x => x.Map<SnackResponseDto>(snack)).Returns(expected);

            // Act
            SnackResponseDto actual = await mockService.Object.GetSnackByIdAsync(id);

            // Assert
            Assert.Equal(expected.Name, actual.Name);
        }

    }
}