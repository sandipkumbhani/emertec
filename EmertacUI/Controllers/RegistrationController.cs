using Emertac.UI.Application.Interface;
using Emertac.UI.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmertacUI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICreateUserService _createUserService;
        private readonly ILoginUserService _loginUserService;
        public RegistrationController(ICreateUserService createUserService, ILoginUserService loginUserService)
        {
            _createUserService = createUserService;
            _loginUserService = loginUserService;
        }
        [HttpGet]
        public IActionResult Registration()
        
        {
            return View(new CreateUserDTO());
        
        }

        [HttpPost]
        public async Task<IActionResult> Registration(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return View(createUserDTO);

            var result = await _createUserService.AddUser(createUserDTO);

            if (result)
            {
                TempData["Success"] = "User created successfully!";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Failed to create user.");

            return View(createUserDTO);
        }
        [HttpGet]
        public IActionResult LoginUser()
        {
            return View(new LoginUserDTO());
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid)
                return View(loginUserDTO);
            var result = await _loginUserService.LoginUserAsync(loginUserDTO);
            if (result)
            {
                TempData["Success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(loginUserDTO);
        }

    }
}
