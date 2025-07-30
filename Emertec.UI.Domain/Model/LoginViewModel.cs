using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        public string Token { get; set; } = "";
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string EmailId { get; set; } = "";
        public string UserRoleName { get; set; } = "";

        public int UserRoleId { get; set; }
        public string? Name { get; set; }
       
    }

}
