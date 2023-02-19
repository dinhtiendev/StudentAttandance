using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentAttandance.Pages.User
{
    //[Authorize(Roles = "1")]
    //[Authorize]
    public class HomeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
