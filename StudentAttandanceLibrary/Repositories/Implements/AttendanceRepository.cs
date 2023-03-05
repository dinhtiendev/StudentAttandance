using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class AttendanceRepository : IAttendanceRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public IQueryable<AttendanceDto> GetAttendancesByCondition(string studentId, int termId, int courseId)
        {
            var query = from a in context.Attendances
                        join s in context.Sessions
                        on a.SessionId equals s.SessionId
                        join g in context.Groups
                        on s.GroupId equals g.GroupId
                        where a.StudentId.Equals(studentId) && g.TermId == termId && g.CourseId == courseId
                        select new AttendanceDto
                        {
                            AttendanceId = a.AttendanceId,
                            SessionId = s.SessionId,
                            Present = a.Present,
                            Description = a.Description,
                            Date = s.Date
                        };
            return query;
        }
    }
}
