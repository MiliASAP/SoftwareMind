namespace WebAPI_SoftwareMind.Models.Api
{
    /// <summary>
    /// Represents the response model for login authentication.
    /// </summary>
    public class LoginResponse
    { 
        /// <summary>
        /// The username of the authenticated user.
        /// </summary>
        /// <example>admin</example>
        public string? UserName { get; set; }
        /// <summary>
        /// The access token provided for authenticated requests.
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ</example>
        public string? AccessToken { get; set; }
        /// <summary>
        /// The expiration time of the access token in seconds.
        /// </summary>
        /// <example>3600</example>
        public int ExpiresIn { get; set; }
    }
}
