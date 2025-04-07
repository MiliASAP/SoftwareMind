using System.ComponentModel.DataAnnotations;
using WebAPI_SoftwareMind.Models;

public class Negotiation
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Proponowana cena musi być większa od 0")]
    public decimal ProposedPrice { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

    public int Attempts { get; set; } = 0;

    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);

    public Product? Product { get; set; }
}