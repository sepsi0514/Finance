using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Models;
using Services.Interfaces.Services;
using WebFinance.Models;
using WebFinance.Services;

namespace WebFinance.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IFirebaseAuthService _authService;
        private readonly IUserService _userService;

        public AuthenticationController(IFirebaseAuthService firebaseAuthService, IUserService userService)
        {
            _authService = firebaseAuthService;
            this._userService = userService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginModel loginModel)
        {
            var token = await _authService.Login(loginModel.Email, loginModel.Password);

            if (token is not null)
            {
                HttpContext.Session.SetString("token", token);
                HttpContext.Session.SetString("user", loginModel.Email);

                return RedirectToAction("Index", "Wallet");
            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Email,Password,Name")] SignUpModel signUpModel)
        {
            var token = await _authService.SignUp(signUpModel.Email, signUpModel.Password);

            if (token is not null)
            {
                HttpContext.Session.SetString("token", token);
                HttpContext.Session.SetString("user", signUpModel.Email);

                await _userService.CreateUser(new UserData() { Email = signUpModel.Email, Name = signUpModel.Name });

                return RedirectToAction("Index", "Wallet");
            }

            return BadRequest();
        }

        public async Task<IActionResult> Logout()
        {
            _authService.SignOut();
            HttpContext.Session.Clear();
            return View();
        }

        public async Task<IActionResult> SignUp() => View();
        public async Task<IActionResult> Login() => View();
    }
}
