using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class ViewScheduleModel : PageModel
    {
        [BindProperty]
        public List<AttandanceDto> listAttandance { get; set; }

        private IAttandanceRepository attandanceRepository;
        public ViewScheduleModel(IAttandanceRepository attandanceRepository)
        {
            this.attandanceRepository = attandanceRepository;
        }
        public IActionResult OnGet(int year, int week)
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                Account? acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (year == 0)
                {
                    year = DateTime.Now.Year;
                }
                List<List<DateTime>> listWeeks = new AutoGenerate().GetWeeks(year);
                if (acc.RoleId == 2)
                {
                    listAttandance = attandanceRepository.GetAttandancesByWeekAndTeacherId(listWeeks[week][0], listWeeks[week][6], acc.AccountId);
                } else if (acc.RoleId == 3)
                {
                    listAttandance = attandanceRepository.GetAttandancesByWeekAndStudentId(listWeeks[week][0], listWeeks[week][6], acc.AccountId);
                }
                ViewData["Weeks"] = listWeeks;
                ViewData["Year"] = year;
                ViewData["Week"] = week;
                return Page();
            }
            return RedirectToPage("/error");
        }
    }
}
