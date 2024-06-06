using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models.DTOs.Snack
{
    public class SnackResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Cost { get; set; }
        public int Quantity { get; set; }
    }
}
