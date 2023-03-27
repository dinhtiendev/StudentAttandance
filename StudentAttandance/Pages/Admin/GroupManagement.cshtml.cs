using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.Admin
{
    public class GroupManagementModel : PageModel
    {
        private IGroupRepository _groupRepository;
        private ISessionRepository _sessionRepository;
        private ITermRepository _termRepository;
        private ICourseRepository _courseRepository;
        private IStudentGroupRepository _studentGroupRepository;

        public GroupManagementModel(IGroupRepository groupRepository, ISessionRepository sessionRepository, ITermRepository termRepository, ICourseRepository courseRepository, IStudentGroupRepository studentGroupRepository)
        {
            _groupRepository = groupRepository;
            _sessionRepository = sessionRepository;
            _termRepository = termRepository;
            _courseRepository = courseRepository;
            _studentGroupRepository = studentGroupRepository;
        }

        public IActionResult OnGet(int termId, int courseId)
        {
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                if (account.RoleId == 1)
                {
                    var terms = _termRepository.GetAllTerms().ToList();
                    ViewData["Terms"] = terms;

                    var courses = _courseRepository.GetAllCourses().ToList();
                    ViewData["Courses"] = courses;

                    if (termId <= 0 || termId > terms.Count)
                    {
                        termId = terms.LastOrDefault().TermId;
                    }
                    ViewData["CurrentTerm"] = termId;
                    TempData["CurrentTerm"] = termId;

                    if (courseId <= 0 || courseId > courses.Count)
                    {
                        courseId = courses.LastOrDefault().CourseId;
                    }
                    ViewData["CurrentCourse"] = courseId;
                    TempData["CurrentCourse"] = courseId;

                    var listGroup = _groupRepository.FilterGroups(termId, courseId).ToList();
                    ViewData["Groups"] = listGroup;
                    return Page();
                }
            }
            return RedirectToPage("/error");
        }

        public IActionResult OnGetDelete(int groupId)
        {
            try
            {
                var g = _groupRepository.GetGroup(groupId);
                if (g != null)
                {
                    _groupRepository.DeleteGroup(groupId);
                    return RedirectToAction("/groupmanagement", new { TermId = (int)TempData["CurrentTerm"], CourseId = (int)TempData["CurrentCourse"] });
                }
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/error");
            }
        }

    }
}
