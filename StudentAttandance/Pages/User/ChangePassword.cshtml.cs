using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Validation.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentAttandance.Pages.User
{
    public class ChangePasswordModel : PageModel
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        [BindProperty]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        [BindProperty]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long")]
        [IsSamePassword("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        [BindProperty]
        public string ConfirmPassword { get; set; }
        private ILogRepository logRepository;
        public ChangePasswordModel(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                var acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (ModelState.IsValid)
                {
                    if (logRepository.ConfirmEmail(acc.Email))
                    {
                        if (logRepository.ChangePassword(acc.Email, OldPassword, NewPassword))
                        {
                            ViewData["Message"] = "Your password has changed!";
                        } else
                        {
                            ViewData["Message"] = "Your password is wrong!";
                        }
                    }
                }
                return Page();
            }
            return RedirectToPage("/error");
            
        }
    }
}
