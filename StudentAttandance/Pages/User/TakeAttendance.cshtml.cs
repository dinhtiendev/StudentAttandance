using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class TakeAttendanceModel : PageModel
    {
        [BindProperty]
        public List<AttandanceDto> Attandances { get; set; }
        private readonly IAttandanceRepository attandanceRepository;
        public TakeAttendanceModel(IAttandanceRepository attandanceRepository)
        {
            this.attandanceRepository = attandanceRepository;
        }
        public IActionResult OnGet(string slotId, string date, int year, int week)
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                Account? acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (acc.RoleId == 2)
                {
                    Attandances = attandanceRepository.GetAttandancesBySlotAndDate(DateTime.Parse(date), Int32.Parse(slotId));
                    ViewData["Year"] = year;
                    ViewData["Week"] = week;
                    return Page();
                }
            }
            return RedirectToPage("/error");
        }

        public IActionResult OnPost(int year, int week, int count, int sessionId)
        {
            var account = HttpContext.Session.GetString("Account");
            if (account != null)
            {
                Account? acc = JsonConvert.DeserializeObject<Account>(account.ToString());
                if (acc.RoleId == 2)
                {
                    var listAttandance = new Dictionary<int, bool>();
                    for (int i = 0; i < count; i++)
                    {
                        listAttandance.Add(Int32.Parse(Request.Form["Id " + i]), Boolean.Parse(Request.Form["Attamdance " + i]));
                    }
                    attandanceRepository.UpdateAttandances(listAttandance, sessionId);
                    return RedirectToPage("viewSchedule", new { year = year, week = week});
                }
            }
            return RedirectToPage("/error");
        }
    }
}
