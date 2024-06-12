using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Snack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [Precision(4, 2)]
        public decimal Cost { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
}
