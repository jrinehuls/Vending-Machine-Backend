using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using VendingMachine.Exceptions;
using VendingMachine.Mappers;
using VendingMachine.Models.DTOs.Funds;
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
        private readonly IMapper _mapper;
        private readonly SnackService _snackService;

        public SnackServiceTest()
        {
            MapperConfiguration config = new (cfg => {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = config.CreateMapper();
            _snackService = new (_mockSnackRepository.Object, _mapper);
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
        public void ThrowIfSoldOut_ThrowsSoldOutEx()
        {
            // Arrange
            Snack snackNoQty = new ()
            {
                Id= 1,
                Name = "Pretzels",
                Cost = 1,
                Quantity = 0
            };

            // Act
            SoldOutException<Snack> ex = Assert.Throws<SoldOutException<Snack>>(() => _snackService.ThrowIfSoldOut(snackNoQty));

            // Assert
            Assert.Equal($"{snackNoQty.Name} is sold out", ex.Message);
            Assert.Equal(422, ex.StatusCode);
        }

        [Fact]
        public void ThrowIfSoldOut_DoesNotThrow()
        {
            // Arrange
            Snack snackNoQty = new()
            {
                Id = 1,
                Name = "Pretzels",
                Cost = 1,
                Quantity = 1
            };

            // Act
            Exception ex = Record.Exception(() => _snackService.ThrowIfSoldOut(snackNoQty));

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public void CalculateFunds_OneOfEachCurrency_Return641()
        {
            // Arrange
            FundsRequestDto requestDto = new()
            {
                Fives = 1,
                Ones = 1,
                Quarters = 1,
                Dimes = 1,
                Nickels = 1,
                Pennies = 1
            };

            int expected = 641;

            // Act
            int actual = _snackService.CalculateFunds(requestDto);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalcChangeOrThrow_FundsLessThanCost_Throw()
        {
            // Arrange
            int funds = 100;
            decimal cost = new decimal(1.01);
            string expectedMessage = $"You only provided $1.00 for a snack that costs $1.01.";

            // Act
            InsufficientFundsException ex = Assert.Throws<InsufficientFundsException>(
                () => _snackService.CalcChangeOrThrow(funds, cost));

            // Assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(400, ex.StatusCode);
        }

        [Fact]
        public void CalcChangeOrThrow_FundsGreaterThanCost_ReturnsChange()
        {
            // Arrange
            int funds = 200;
            decimal cost = new decimal(1.99);
            int expected = 1;

            // Act
            int actual = _snackService.CalcChangeOrThrow(funds, cost);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalcChangeOrThrow_FundsEqualCost_ReturnsZero()
        {
            // Arrange
            int funds = 200;
            decimal cost = new decimal(2.00);
            int expected = 0;

            // Act
            int actual = _snackService.CalcChangeOrThrow(funds, cost);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateChangeResponse_641_ReturnsOneOfEachCurrency()
        {
            // Arrange
            int input = 641;
            FundsResponseDto expected = new()
            {
                TotalChange = 6.41,
                Fives = 1,
                Ones = 1,
                Quarters = 1,
                Dimes = 1,
                Nickels = 1,
                Pennies = 1
            };

            // Act
            FundsResponseDto actual = _snackService.CalculateChangeResponse(input);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public async void GetSnackByIdAsync_ReturnsSnackResponseDto()
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

            Mock<SnackService> mockService = new (_mockSnackRepository.Object, _mapper);
            mockService.Setup(x => x.FindByIdOrThrowAsync(id)).ReturnsAsync(snack);

            // Act
            SnackResponseDto actual = await mockService.Object.GetSnackByIdAsync(id);

            // Assert
            Assert.Equal(expected.ToString(), actual.ToString());
        }

    }
}