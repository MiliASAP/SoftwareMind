using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }

}
