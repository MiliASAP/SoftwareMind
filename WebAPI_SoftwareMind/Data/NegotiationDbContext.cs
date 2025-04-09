using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Models.Entities;

public class NegotiationDbContext : DbContext
{
    public NegotiationDbContext(DbContextOptions<NegotiationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Negotiation> Negotiations { get; set; }
    public DbSet<Employee> Employees { get; set; }

}