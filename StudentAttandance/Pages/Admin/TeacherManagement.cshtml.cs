using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.ModelValidation;

namespace StudentAttandance.Pages.Admin
{
    public class LecturerManagementModel : PageModel
    {
        [BindProperty]
        public IFormFile File { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostAdd()
        {
            if (File == null)
            {
                return Redirect("/login");
            }
            // Form submission code here
            return Redirect("/home");
        }

        public IActionResult OnGetDelete()
        {
            // Form submission code here
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate()
        {
            // Form submission code here
            return RedirectToPage();
        }
    }
}
