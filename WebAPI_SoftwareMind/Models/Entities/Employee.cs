using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Entities
{
    /// <summary>
    /// Represents an employee in the system.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// The unique identifier of the employee.
        /// </summary>
        /// <example>1</example>
        [Key]
        public int EmployeeId { get; set; }
        /// <summary>
        /// The username of the employee for login purposes.
        /// </summary>
        /// <example>admin</example>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// The password hash of the employee. It should be securely stored.
        /// </summary>
        /// <example>AQAAAAIAAYagAAAAEBQpka2B/elN4o/MsP7coj7lpwfbbHqZrfcBzAqW91eV3ZEXb0cFjqIX+bxg0cLk3g==</example>
        public string PasswordHash { get; set; } = string.Empty;
    }

}
