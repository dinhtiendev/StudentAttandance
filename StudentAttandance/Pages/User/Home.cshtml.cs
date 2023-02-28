using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentAttandance.Pages.User
{
    public class HomeModel : PageModel
    {
        
        public IActionResult OnGet()
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                return Page();
            }
            return RedirectToPage("/error");
        }
    }
}
