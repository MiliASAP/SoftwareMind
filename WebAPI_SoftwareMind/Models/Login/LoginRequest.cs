using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Api
{
    /// <summary>
    /// Represents the request model for logging in.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        /// <example>admin</example>
        [Required]
        public string? Username { get; set; }
        /// <summary>
        /// The password of the user.
        /// </summary>
        /// <example>admin123</example>
        [Required]
        public string? Password { get; set; }
    }
}
