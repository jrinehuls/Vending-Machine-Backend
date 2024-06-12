using VendingMachine.Models.DTOs.Funds;
using VendingMachine.Models.DTOs.Snack;

namespace VendingMachine.Services
{
    public interface ISnackService
    {
        Task<List<SnackResponseDto>> GetAllSnacksAsync();
        Task<SnackResponseDto> GetSnackByIdAsync(long id);
        Task<SnackChangeResponseDto> PurchaseSnackAsync(long id, FundsRequestDto requestDto);
    }
}
