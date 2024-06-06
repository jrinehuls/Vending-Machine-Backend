namespace VendingMachine.Models.DTOs.Snack
{
    public class SnackChangeResponseDto
    {
        public SnackResponseDto? SnackResponseDto { get; set; }
        public double Change { get; set; }
        public int Quarters { get; set; }

    }
}
