using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.Implements;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class ActivityDetailsModel : PageModel
    {
        [BindProperty]
        public AttandanceDto Attandance { get; set; }

        public IAttandanceRepository attandanceRepository;
        public ActivityDetailsModel(IAttandanceRepository attandanceRepository)
        {
            this.attandanceRepository = attandanceRepository;
        }
        public IActionResult OnGet(string attendanceId)
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                Account? acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (acc.RoleId == 3)
                {
                    Attandance = attandanceRepository.GetAttandanceById(Int32.Parse(attendanceId));
                    return Page();
                }
            }
            return RedirectToPage("/error");
        }
    }
}
