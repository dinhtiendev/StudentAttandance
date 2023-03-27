using StudentAttandanceLibrary.CommonFunctions;
using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class SessionRepository : ISessionRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddSessions(List<Session> sessions)
        {
            context.AddRange(sessions);
            context.SaveChanges();
        }

        public IQueryable<SessionDto> GetAllSessionsByCondition(string studentId, int termId, int courseId)
        {
            var query = from s in context.Sessions
                        join g in context.Groups
                        on s.GroupId equals g.GroupId
                        join sg in context.StudentGroups
                         on g.GroupId equals sg.GroupId
                        join ts in context.TimeSlots
                        on s.TimeSlotId equals ts.TimeSlotId
                        join r in context.Rooms
                        on s.RoomId equals r.RoomId
                        //join a in context.Attendances
                        //on session.SessionId equals a.SessionId
                        where g.TermId == termId && g.CourseId == courseId && sg.StudentId.Equals(studentId)
                        select new SessionDto
                        {
                            SessionId = s.SessionId,
                            GroupName = g.GroupName,
                            TimeSlotId = ts.TimeSlotId,
                            Description = ts.Description,
                            RoomName = r.RoomName,
                            TeacherId = s.TeacherId,
                            Date = s.Date,
                            DayOfWeek = ConvertDataTypes.ConvertDateToDayOfWeek(s.Date),
                            Index = s.Index,
                            Attanded = (bool)s.Attanded,
                            //Present = a.Present,
                            //Comment = a.Description
                        };
            return query;
        }
    }
}
