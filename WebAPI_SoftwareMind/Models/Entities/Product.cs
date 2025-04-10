using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities
{
    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The unique identifier of the product.
        /// </summary>
        /// <example>12</example>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        /// <example>Laptop</example>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The price of the product.
        /// </summary>
        /// <example>999.99</example>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public decimal Price { get; set; }
    }
}
