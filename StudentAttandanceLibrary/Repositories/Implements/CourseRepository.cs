using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class CourseRepository : ICourseRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> GetAllCourses()
        {
            var query = context.Courses;
            return query;
        }

        public IQueryable<CourseDto> GetCoursesByCondition(string studentId, int termId)
        {
            var query = (from c in context.Courses
                         join g in context.Groups
                         on c.CourseId equals g.CourseId
                         join sg in context.StudentGroups
                         on g.GroupId equals sg.GroupId
                         join s in context.Sessions
                         on g.GroupId equals s.GroupId
                         where sg.StudentId.Equals(studentId) && g.TermId == termId && s.Index == 1
                         select new CourseDto
                         {
                             CourseId = c.CourseId,
                             CourseCode = c.CourseCode,
                             CourseName = c.CourseName,
                             GroupName = g.GroupName,
                             StartDate = s.Date
                         }).Distinct();
            return query;
        }

        public List<Models.Course> GetListCourses()
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }
    }
}
