using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
                List<List<DateTime>> listWeeks = getWeeks(year);
                if (acc.RoleId == 2)
                {

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

        private List<List<DateTime>> getWeeks(int year)
        {
            var weeks = new List<List<DateTime>>();
            DateTime startOfYear = new DateTime(year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1).AddDays(-1);

            DateTime startDate = startOfYear;
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(1);
            }

            DateTime endDate = endOfYear;
            while (endDate.DayOfWeek != DayOfWeek.Sunday)
            {
                endDate = endDate.AddDays(1);
            }

            DateTime weekStartDate = startDate;
            while (weekStartDate <= endDate)
            {
                var week = new List<DateTime>();
                weeks.Add(week);
                int i = 1;
                while (i <= 7)
                {
                    week.Add(weekStartDate);
                    weekStartDate = weekStartDate.AddDays(1);
                    i++;
                }
            }
            return weeks;
        }
    }
}
