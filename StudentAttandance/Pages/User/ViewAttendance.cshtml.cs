using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class ViewAttendanceModel : PageModel
    {
        private ICourseRepository _courseRepository;
        private ITermRepository _termRepository;
        private ISessionRepository _sessionRepository;
        private IAttendanceRepository _attendanceRepository;

        public ViewAttendanceModel(ITermRepository termRepository, ICourseRepository courseRepository, ISessionRepository sessionRepository, IAttendanceRepository attendanceRepository)
        {
            _termRepository = termRepository;
            _courseRepository = courseRepository;
            _sessionRepository = sessionRepository;
            _attendanceRepository = attendanceRepository;
        }

        public void OnGet(int termId, int courseId)
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                ViewData["TermList"] = _termRepository.GetAllTerms().ToList();
                if (termId == 0 || termId > _termRepository.GetAllTerms().Count())
                {
                    termId = _termRepository.GetAllTerms().OrderBy(x => x.TermId).LastOrDefault().TermId;
                }
                ViewData["CurrentTermId"] = termId;

                var courses = _courseRepository.GetCoursesByCondition(account.AccountId, termId).ToList();
                if (courses.Count > 0)
                {
                    ViewData["CourseList"] = courses;
                    if (courseId == 0)
                    {
                        courseId = courses.FirstOrDefault().CourseId;
                    }
                    ViewData["CurrentCourseId"] = courseId;

                    var sessions = _sessionRepository.GetAllSessionsByCondition(account.AccountId, termId, courseId).ToList();
                    ViewData["SessionList"] = sessions;

                    var attendances = _attendanceRepository.GetAttendancesByCondition(account.AccountId, termId, courseId).ToList();
                    ViewData["AttendanceList"] = attendances;

                    var numberAbsent = attendances.Where(x => x.Present == false).Count();
                    ViewData["NumberAbsent"] = numberAbsent;
                    var totalAbsent = Calculation.NumberAbsent(sessions.Count, numberAbsent);
                    ViewData["TotalAbsent"] = totalAbsent;
                }
                else
                {
                    ViewData["CurrentCourseId"] = -1;
                    ViewData["CourseList"] = null;
                }
            }
        }
    }
}
