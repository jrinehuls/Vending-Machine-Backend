using VendingMachine.Models.DTOs;

namespace VendingMachine.Services
{
    public interface ISnackService
    {
        Task<List<SnackResponseDto>> GetSnacksAsync();
        Task<SnackResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto);
    }
}
