using VendingMachine.Models.DTOs.Snack;

namespace VendingMachine.Services
{
    public interface ISnackService
    {
        Task<List<SnackResponseDto>> GetSnacksAsync();
        Task<SnackResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto);
    }
}
