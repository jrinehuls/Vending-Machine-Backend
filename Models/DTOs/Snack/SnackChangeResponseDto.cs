using VendingMachine.Models.DTOs.Funds;

namespace VendingMachine.Models.DTOs.Snack
{
    public class SnackChangeResponseDto
    {
        public SnackResponseDto? Snack { get; set; }
        public FundsResponseDto? Change { get; set; }

    }
}
