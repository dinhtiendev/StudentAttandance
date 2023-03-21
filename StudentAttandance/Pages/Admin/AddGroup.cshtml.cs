using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandance.Pages.Admin
{
    public class AddGroupModel : PageModel
    {
        private IGroupRepository _groupRepository;
        private ISessionRepository _sessionRepository;
        private ITermRepository _termRepository;
        private ICourseRepository _courseRepository;
        private IStudentGroupRepository _studentGroupRepository;
        private ITeacherRepository _teacherRepository;
        private IStudentRepository _studentRepository;
        private IRoomRepository _roomRepository;
        private ITimeSlotRepository _timeSlotRepository;

        public AddGroupModel(IGroupRepository groupRepository, ISessionRepository sessionRepository, ITermRepository termRepository,
            ICourseRepository courseRepository, IStudentGroupRepository studentGroupRepository, ITeacherRepository teacherRepository
            , IRoomRepository roomRepository, ITimeSlotRepository timeSlotRepository, IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _sessionRepository = sessionRepository;
            _termRepository = termRepository;
            _courseRepository = courseRepository;
            _studentGroupRepository = studentGroupRepository;
            _teacherRepository = teacherRepository;
            _roomRepository = roomRepository;
            _timeSlotRepository = timeSlotRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["Teachers"] = _teacherRepository.GetAllTeachers().ToList();
            ViewData["Terms"] = _termRepository.GetAllTerms().ToList();
            ViewData["Course"] = _courseRepository.GetAllCourses().ToList();
            ViewData["Rooms"] = _roomRepository.GetRooms().ToList();
            ViewData["Slots"] = _timeSlotRepository.GetTimeSlots().ToList();
            ViewData["K"] = DBHelper.GetAllK(_studentRepository.GetStudents());
            return Page();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
