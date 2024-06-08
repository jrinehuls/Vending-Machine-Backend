using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Data;
using VendingMachine.Exceptions;
using VendingMachine.Models.DTOs.Funds;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Models.Entities;
using VendingMachine.Models.Enums;

namespace VendingMachine.Services.Impl
{
    public class SnackService : ISnackService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SnackService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SnackResponseDto>> GetAllSnacksAsync()
        {
            List<SnackResponseDto> responseDtos = await _context.Snacks
                .Select(s => _mapper.Map<SnackResponseDto>(s))
                .ToListAsync();
            return responseDtos;
        }

        public async Task<SnackResponseDto> GetSnackByIdAsync(long id)
        {
            Snack snack = await FindByIdOrThrowAsync(id);
            SnackResponseDto responseDto = _mapper.Map<SnackResponseDto>(snack);
            return responseDto;
        }

        public async Task<SnackChangeResponseDto> PurchaseSnackAsync(long id, FundsRequestDto requestDto)
        {
            Snack snack = await FindByIdOrThrowAsync(id);
            ThrowIfSoldOut(snack);

            int funds = CalculateFunds(requestDto);
            int change = CalcChangeOrThrow(funds, snack.Cost);

            snack.Quantity--;
            await _context.SaveChangesAsync();

            SnackResponseDto snackResponse = _mapper.Map<SnackResponseDto>(snack);
            FundsResponseDto changeResponse = CalculateChangeResponse(change);
            SnackChangeResponseDto responseDto = new()
            {
                Snack = snackResponse,
                Change = changeResponse
            };

            return responseDto;
        }

        // Private methods

        private async Task<Snack> FindByIdOrThrowAsync(long id)
        {
            Snack? snack = await _context.Snacks.FirstOrDefaultAsync(s => s.Id == id);
            if (snack is null)
            {
                throw new SnackNotFoundException(id);
            }
            return snack;
        }

        private void ThrowIfSoldOut(Snack snack)
        {
            if (snack.Quantity < 1)
            {
                throw new SoldOutException();
            }
        }

        private int CalculateFunds(FundsRequestDto requestDto)
        {
            int funds = 0;
            funds += requestDto.Fives * (int)Currency.Five;
            funds += requestDto.Ones * (int)Currency.One;
            funds += requestDto.Quarters * (int)Currency.Quarter;
            funds += requestDto.Dimes * (int)Currency.Dime;
            funds += requestDto.Nickels * (int)Currency.Nickel;
            funds += requestDto.Pennies * (int)Currency.Penny;

            return funds;
        }

        private int CalcChangeOrThrow(int funds, double cost) 
        {
            int change = funds - (int)(100*cost);
            if (change < 0) {
                throw new InsufficientFundsException(funds, cost);
            }
            return change;
        }

        private FundsResponseDto CalculateChangeResponse(int change)
        {

            FundsResponseDto response = new FundsResponseDto();

            response.TotalChange = Math.Round((double)(change/100.0), 2, MidpointRounding.AwayFromZero);

            int remainingChange = change;
            response.Fives = remainingChange / (int)Currency.Five;

            remainingChange %= (int)Currency.Five;
            response.Ones = remainingChange / (int)Currency.One;

            remainingChange %= (int)Currency.One;
            response.Quarters = remainingChange / (int)Currency.Quarter;

            remainingChange %= (int)Currency.Quarter;
            response.Dimes = remainingChange / (int)Currency.Dime;

            remainingChange %= (int)Currency.Dime;
            response.Nickles = remainingChange / (int)Currency.Nickel;

            remainingChange %= (int)Currency.Nickel;
            response.Pennies = remainingChange / (int)Currency.Penny;

            return response;
        }

    }
}
