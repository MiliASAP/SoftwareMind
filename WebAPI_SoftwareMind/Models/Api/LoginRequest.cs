using System.ComponentModel.DataAnnotations;

namespace WebAPI_SoftwareMind.Models.Api
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
