
using System.ComponentModel.DataAnnotations;

namespace Emertec.UI.Domain.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string? EmailId { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
    public class ResponseToken
    {
        public string? Token { get; set; }
    }
}
