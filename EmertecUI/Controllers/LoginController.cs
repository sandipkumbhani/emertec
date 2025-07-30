using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public IActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var responseToken = await _loginServices.Login(viewModel);
                    Response.Cookies.Append("jwtToken", responseToken.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddHours(24)
                    });
                    //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(responseToken.Token);
                    //var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "unique_name")?.Value;
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Email,email ?? viewModel.EmailId)
                    //};
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(responseToken.Token);

                    var claims = new List<Claim>
                    {
                        new Claim("UserId", responseToken.UserId.ToString()),
                        new Claim(ClaimTypes.Name, responseToken.Username),
                        new Claim(ClaimTypes.Email, responseToken.EmailId),
                        new Claim(ClaimTypes.Role, responseToken.UserRoleName),


                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Redirect("~/User/UserList");
                }
                else
                {
                    ViewBag.LoginMessage = "";
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.LoginMessage = ex.Message;
                return View(viewModel);
            }
        }
    }
}
