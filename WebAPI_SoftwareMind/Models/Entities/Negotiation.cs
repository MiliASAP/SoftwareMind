using System.ComponentModel.DataAnnotations;
using WebAPI_SoftwareMind.Models.Entities;

public class Negotiation
{
    [Key]
    public int NegotiationId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public decimal ProposedPrice { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

    public int Attempts { get; set; } = 0;

    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);

    public Product? Product { get; set; }
}