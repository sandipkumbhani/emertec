
using System.ComponentModel.DataAnnotations;

namespace Emertac.UI.Domain.DTO
{
    public class CreateUserDTO
    {

        [Required(ErrorMessage = "UserName is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com", ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }
        
    }
}
