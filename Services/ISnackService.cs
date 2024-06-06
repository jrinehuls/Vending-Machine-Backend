using VendingMachine.Models.DTOs.Snack;

namespace VendingMachine.Services
{
    public interface ISnackService
    {
        Task<List<SnackResponseDto>> GetSnacksAsync();
        Task<SnackChangeResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto);
    }
}
