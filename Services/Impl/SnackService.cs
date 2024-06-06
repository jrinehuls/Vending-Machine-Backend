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

        public async Task<SnackResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto)
        {
            Snack? snack = await _context.Snacks.FirstOrDefaultAsync(s => s.Id == id);
            if (snack == null)
            {
                throw new SnackNotFoundException(id);
            }

            if (snack.Quantity < 1)
            {
                throw new SoldOutException();
            }

            double providedFunds = CalculateFunds(requestDto);

            if (providedFunds < snack.Cost)
            {
                throw new InsufficientFundsException(providedFunds, snack);
            }

            snack.Quantity--;
            await _context.SaveChangesAsync();

            return _mapper.Map<SnackResponseDto>(snack);
        }

        private double CalculateFunds(PurchaseSnackRequestDto requestDto)
        {
            double funds = 0;
            funds += requestDto.Fives * 5.00;
            funds += requestDto.Ones * 1.00;
            funds += requestDto.Quarters * 0.25;

            return funds;
        }
    }
}
