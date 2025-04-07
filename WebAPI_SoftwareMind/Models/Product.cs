using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public decimal Price { get; set; }
    }
}
