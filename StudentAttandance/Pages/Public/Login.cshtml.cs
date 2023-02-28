using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace StudentAttandance.Pages.Public
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
        public IActionResult OnGet(string message)
        {
            if (message != null)
            {
                ViewData["Message"] = message;
            }
            HttpContext.Session.Remove("Account");
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Account? account = logRepository.Login(Email, Password);
                if (account != null)
                {
                    HttpContext.Session.SetString("Account", JsonConvert.SerializeObject(account));
                    return RedirectToPage("/user/home");
                }
            }
            ViewData["Message"] = "Your email or password is wrong!";
            return Page();
        }
    }
}
