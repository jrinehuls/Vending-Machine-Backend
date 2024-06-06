using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models.DTOs
{
    public class PurchaseSnackRequestDto
    {

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Ones { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Fives { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Quarters { get; set; }

    }
}
