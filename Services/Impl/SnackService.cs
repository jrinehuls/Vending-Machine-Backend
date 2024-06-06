using AutoMapper;
using VendingMachine.Data;
using VendingMachine.Models.DTOs;

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

        public Task<List<SnackResponseDto>> GetSnacksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SnackResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
