using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models.DTOs.Snack
{
    public class PurchaseSnackRequestDto
    {

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be at least {1}.")]
        public int Ones { get; set; } = -1;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be at least {1}.")]
        public int Fives { get; set; } = -1;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be at least {1}.")]
        public int Quarters { get; set; } = -1;

    }
}
