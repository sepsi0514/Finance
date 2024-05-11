using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WebFinance.DTOS;
using WebFinance.Services;

namespace WebFinance.Models
{
    public class SignUpModel : PageModel
    {
        private readonly IFirebaseAuthService _authService;

        [BindProperty]
        public SignUpUserDto UserDto { get; set; }

        public SignUpModel(IFirebaseAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = await _authService.SignUp(UserDto.Email, UserDto.Password);

            if (token is not null)
            {
                HttpContext.Session.SetString("token", token);
                return RedirectToPage("/AuthenticatedPage");
            }

            return BadRequest();
        }
    }
}
