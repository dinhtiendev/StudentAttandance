using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.User
{
    public class GroupListModel : PageModel
    {

        private IStudentRepository _studentRepository;
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;
        private ITermRepository _termRepository;

        public GroupListModel(IStudentRepository studentRepository, IGroupRepository groupRepository, ICourseRepository courseRepository, ITermRepository termRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
            _termRepository = termRepository;
        }
        public void OnGet(int termId, int courseId, int groupId)
        {
            ViewData["TermList"] = _termRepository.GetAllTerms().ToList();
            if (termId == 0 || termId > _termRepository.GetAllTerms().Count())
            {
                termId = _termRepository.GetAllTerms().OrderBy(x => x.TermId).LastOrDefault().TermId;
            }
            ViewData["CurrentTermId"] = termId;

            ViewData["CourseList"] = _courseRepository.GetAllCourses().ToList();
            if (courseId == 0 || courseId > _courseRepository.GetAllCourses().Count())
            {
                courseId = _courseRepository.GetAllCourses().ToList().OrderBy(x => x.CourseId).FirstOrDefault().CourseId;
            }
            ViewData["CurrentCourseId"] = courseId;

            ViewData["GroupList"] = _groupRepository.GetGroupsByTermAndCourse(termId, courseId).ToList();
            ViewData["CurrentGroupId"] = groupId;

            List<Student> list = _studentRepository.GetStudentsByConditions(termId, courseId, groupId).ToList();
            if (courseId != 0)
            {
                ViewData["StudentList"] = list;
            } else
            {
                ViewData["StudentList"] = null;
            }

            //return Page();
            //return RedirectToPage("/grouplist?id="+termId+"&param1="+courseId);
        }

    }
}
