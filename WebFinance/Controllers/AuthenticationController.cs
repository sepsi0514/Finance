using Microsoft.AspNetCore.Mvc;
using WebFinance.Models;
using WebFinance.Services;

namespace WebFinance.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IFirebaseAuthService _authService;

        public AuthenticationController(IFirebaseAuthService firebaseAuthService)
        {
            _authService = firebaseAuthService;
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
        public async Task<IActionResult> Create([Bind("Email,Password")] LoginModel loginModel)
        {
            var token = await _authService.SignUp(loginModel.Email, loginModel.Password);

            if (token is not null)
            {
                HttpContext.Session.SetString("token", token);
                HttpContext.Session.SetString("user", loginModel.Email);

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
