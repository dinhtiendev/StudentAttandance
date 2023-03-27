using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Validation.ModelValidation;

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
            if (HttpContext.Session.GetString("Account") != null)
            {
                var account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("Account"));

                if (account.RoleId == 1)
                {
                    //ViewData["Teachers"] = _teacherRepository.GetTeachers().ToList();
                    ViewData["Terms"] = _termRepository.GetAllTerms().ToList();
                    ViewData["Course"] = _courseRepository.GetAllCourses().ToList();
                    //ViewData["Rooms"] = _roomRepository.GetRooms().ToList();
                    //ViewData["Slots"] = _timeSlotRepository.GetTimeSlots().ToList();
                    ViewData["KS"] = DBHelper.GetAllK(_studentRepository.GetStudents()).ToList();
                    return Page();
                }
            }
            return RedirectToPage("/error");
        }

        public IActionResult OnPost(AddGroupValidation addGroupValidation)
        {
            var checkExist = _groupRepository.GetGroupsByTermAndCourse(addGroupValidation.TermId, addGroupValidation.CourseId).ToList();
            if (checkExist.Any())
            {
                ViewData["Message"] = "Duplicate !!!";
                return OnGet();
            }

            var k = addGroupValidation.KS;
            var students = _studentRepository.GetStudentsByK(k);

            int totalStudentInK = students.ToList().Count();
            int totalStudentsInGroup = 20;
            int totalGroup = (int)Math.Ceiling((double)totalStudentInK / totalStudentsInGroup);

            int groupIndex = 0;
            int rowSkip = 0;

            int numberGroupsInTerm = _groupRepository.NumberGroupsInTerm(k.Substring(1,2),addGroupValidation.TermId);
            int status = DBHelper.NumberGroupsInTerm(numberGroupsInTerm);

            var addSessions = new List<Session>();
            var addStudentGroups = new List<StudentGroup>();

            var teachers = _teacherRepository.GetAllTeachers().ToList();

            for (int i = 0; i < totalGroup; i++)
            {
                groupIndex++;
                string groupName = "SE" + k.Substring(1, 2) + ((groupIndex >= 1 && groupIndex <= 9) ? ("0" + groupIndex) : groupIndex);
                string teacherId = "";
                
                teacherId = teachers.FirstOrDefault().TeacherId;

                var group = new Group
                {
                    TeacherId = teacherId,
                    GroupName = groupName,
                    TermId = Convert.ToInt32(addGroupValidation.TermId),
                    CourseId = Convert.ToInt32(addGroupValidation.CourseId),
                };

                _groupRepository.AddGroup(group);

                var currentGroup = _groupRepository.GetGroupsByConditions(groupName, Convert.ToInt32(addGroupValidation.TermId), Convert.ToInt32(addGroupValidation.CourseId)).FirstOrDefault();
                var divide = _studentRepository.DivideStudents(k, rowSkip, totalStudentsInGroup).ToList();

                for (int j = 0; j < divide.Count; j++)
                {
                    var sg = new StudentGroup
                    {
                        GroupId = currentGroup.GroupId,
                        StudentId = divide[j].StudentId,
                    };
                    addStudentGroups.Add(sg);
                }
                divide.Clear();
                rowSkip = addStudentGroups.Count;

                DateTime currentDate = new DateTime();
                DateTime newDate = new DateTime();

                var room = _roomRepository.GetRooms().FirstOrDefault();
                for (int o = 0; o < addGroupValidation.NumberSlot; o++)
                {
                    var x = DBHelper.GetDateForSession(addGroupValidation.DateStart, currentDate, newDate, groupIndex, o, status);
                    var session = new Session
                    {
                        GroupId = currentGroup.GroupId,
                        Attanded = false,
                        Index = o + 1,
                        RoomId = room.RoomId,
                        TeacherId = teacherId,
                        TimeSlotId = DBHelper.GetTimeSlot(groupIndex, status),
                        Date = x,
                    };
                    addSessions.Add(session);
                    currentDate = x;
                }
            }

            _studentGroupRepository.AddStudentGroups(addStudentGroups);
            _sessionRepository.AddSessions(addSessions);

            return RedirectToPage("/admin/groupmanagement");
        }
    }
}
