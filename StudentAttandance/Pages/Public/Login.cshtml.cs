using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System.ComponentModel.DataAnnotations;
using StudentAttandanceLibrary.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace StudentAttandance.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogRepository logRepository;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [BindProperty]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        [BindProperty]
        public string Password { get; set; }
        public LoginModel(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public void OnGet(string message)
        {
            if (message != null)
            {
                ViewData["Message"] = message;
            }
            TempData.Remove("Account");
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                Account? account = logRepository.Login(Email, Password);
                if (account != null)
                {
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, account.AccountId),
                    //    new Claim(ClaimTypes.Role, account.RoleId.ToString())
                    //};

                    //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //var principal = new ClaimsPrincipal(identity);

                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    TempData.Add("Account", JsonConvert.SerializeObject(account));
                    return RedirectToPage("/user/home");
                }
            }
            ViewData["Message"] = "Your email or password is wrong!";
            return Page();
        }
    }
}
