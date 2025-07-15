using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Models;
using Emertec.UI.Domain.Helper;
using Microsoft.Extensions.Configuration;


namespace EmertecUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginServices _loginServices;
        private readonly IConfiguration _configuration;
        private ApplicationURL applicationURL;
        public LoginController(ILoginServices loginServices, IConfiguration configuration)
        {
            _loginServices = loginServices;
            _configuration = configuration;
            applicationURL = new ApplicationURL(configuration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var tokenString = await _loginServices.Login(viewModel);
                if (!string.IsNullOrEmpty(tokenString))
                {
                    Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Use HTTPS
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(24)
                    });
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
                    var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "unique_name")?.Value;
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,email ?? viewModel.Email)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("AddUser");
                }
                ViewData["LoginMessage"] = "Invalid username or password..!";
                ViewBag.appUrl = applicationURL.url;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        
        }
        public IActionResult MenuMaster()
        {
            return View();
        }

    }
}
