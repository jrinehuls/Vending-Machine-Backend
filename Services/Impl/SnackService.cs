using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Data;
using VendingMachine.Exceptions;
using VendingMachine.Models.DTOs.Funds;
using VendingMachine.Models.DTOs.Snack;
using VendingMachine.Models.Entities;

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

            double funds = CalculateFunds(requestDto);
            double change = CalcChangeOrThrow(funds, snack.Cost);

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

        private double CalculateFunds(FundsRequestDto requestDto)
        {
            double funds = 0;
            funds += requestDto.Fives * 5.00;
            funds += requestDto.Ones * 1.00;
            funds += requestDto.Quarters * 0.25;
            funds += requestDto.Dimes * 0.10;
            funds += requestDto.Nickels * 0.05;
            funds += requestDto.Pennies * 0.01;

            return Math.Round(funds, 2, MidpointRounding.AwayFromZero);
        }

        private double CalcChangeOrThrow(double funds, double cost) 
        {
            double change = funds - cost;
            if (change < 0) {
                throw new InsufficientFundsException(funds, cost);
            }
            return Math.Round(change, 2, MidpointRounding.AwayFromZero);
        }

        private FundsResponseDto CalculateChangeResponse(double change)
        {

            FundsResponseDto response = new FundsResponseDto();

            response.TotalChange = change;

            int remainingChange = (int)(change*100);
            response.Fives = (int)(remainingChange / 500);

            remainingChange %= 500;
            response.Ones = (int)(remainingChange / 100);

            remainingChange %= 100;
            response.Quarters = (int)(remainingChange / 25);

            remainingChange %= 25;
            response.Dimes = (int)(remainingChange / 10);

            remainingChange %= 10;
            response.Nickles = (int)(remainingChange / 5);

            remainingChange %= 5;
            response.Pennies = (int)(remainingChange / 1);

            return response;
        }

    }
}
