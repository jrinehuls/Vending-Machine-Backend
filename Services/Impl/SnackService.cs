using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Data;
using VendingMachine.Exceptions;
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

        public async Task<List<SnackResponseDto>> GetSnacksAsync()
        {
            List<SnackResponseDto> responseDtos = await _context.Snacks
                .Select(s => _mapper.Map<SnackResponseDto>(s))
                .ToListAsync();
            return responseDtos;
        }

        public async Task<SnackChangeResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto)
        {
            Snack snack = await FindByIdOrThrowAsync(id);
            ThrowIfSoldOut(snack);

            double funds = CalculateFunds(requestDto);
            double change = CalcChangeOrThrow(funds, snack.Cost);

            snack.Quantity--;
            await _context.SaveChangesAsync();

            SnackResponseDto snackResponse = _mapper.Map<SnackResponseDto>(snack);
            ChangeResponseDto changeResponse = CalculateChangeResponse(change);
            SnackChangeResponseDto responseDto = new()
            {
                SnackResponseDto = snackResponse,
                ChangeResponseDto = changeResponse
            };

            return responseDto;
        }

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

        private double CalculateFunds(PurchaseSnackRequestDto requestDto)
        {
            double funds = 0;
            funds += requestDto.Fives * 5.00;
            funds += requestDto.Ones * 1.00;
            funds += requestDto.Quarters * 0.25;

            return funds;
        }

        private double CalcChangeOrThrow(double funds, double cost) 
        {
            double change = funds - cost;
            if (change < 0) {
                throw new InsufficientFundsException(funds, cost);
            }
            return change;
        }

        private ChangeResponseDto CalculateChangeResponse(double change)
        {
            ChangeResponseDto response = new ChangeResponseDto();
            response.Change = change;
            response.Quarters = (int)(change / 0.25);

            return response;
        }
    }
}
