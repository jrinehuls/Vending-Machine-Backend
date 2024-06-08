using VendingMachine.Models.DTOs.Snack;

namespace VendingMachine.Services
{
    public interface ISnackService
    {
        Task<List<SnackResponseDto>> GetAllSnacksAsync();
        Task<SnackResponseDto> GetSnackByIdAsync(long id);
        Task<SnackChangeResponseDto> PurchaseSnackAsync(long id, PurchaseSnackRequestDto requestDto);
    }
}
