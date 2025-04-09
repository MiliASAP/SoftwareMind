using Microsoft.AspNetCore.Identity;
using WebAPI_SoftwareMind.Models.Entities;
using WebApiDemo.Handlers;

public static class DbInitializer
{
    public static void Seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NegotiationDbContext>();

        context.Database.EnsureCreated(); // tworzy bazę jeśli nie istnieje

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Laptop Dell", Price = 4500 },
                new Product { Name = "Monitor LG", Price = 900 },
                new Product { Name = "Mysz Logitech", Price = 150 }
            );

            context.SaveChanges();
        }

        if (!context.Employees.Any())
        {
            var hasher = new PasswordHasher<Employee>();
            var employee = new Employee
            {
                Username = "admin",
                PasswordHash = PasswordHashHandler.HashPassword("admin123")
            };

           

            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}