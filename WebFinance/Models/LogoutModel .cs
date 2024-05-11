using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WebFinance.Services;

namespace WebFinance.Models
{
    public class LogoutModel : PageModel
    {
        private readonly IFirebaseAuthService _firebaseAuthService;

        public LogoutModel(IFirebaseAuthService firebaseAuthService)
        {
            _firebaseAuthService = firebaseAuthService;
        }

        public IActionResult OnGet()
        {
            _firebaseAuthService.SignOut();

            HttpContext.Session.Remove("token");

            return Page();
        }
    }
}
