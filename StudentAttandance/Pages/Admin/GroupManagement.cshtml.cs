using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var terms = _termRepository.GetAllTerms();
            ViewData["Terms"] = terms.ToList();

            var courses = _courseRepository.GetAllCourses();
            ViewData["Courses"] = courses.ToList();

            if (termId <= 0 || termId > terms.Count())
            {
                termId = terms.LastOrDefault().TermId;
            }

            if (courseId <= 0 || courseId > courses.Count())
            {
                courseId = courses.LastOrDefault().CourseId;
            }


            return Page();
        }

    }
}
