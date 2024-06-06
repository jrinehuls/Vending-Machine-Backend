using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models.DTOs
{
    public class SnackResponseDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double Cost { get; set; }
        public int Quantity { get; set; }
    }
}
