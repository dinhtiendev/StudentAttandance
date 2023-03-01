using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAttandanceLibrary.Models;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class AttandanceRepository : IAttandanceRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public AttandanceDto GetAttandanceById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AttandanceDto> GetAttandancesBySlotIdAndGroupId(int slotId, int groupId)
        {
            throw new NotImplementedException();
        }

        public List<AttandanceDto> GetAttandancesByWeekAndStudentId(DateTime startDate, DateTime endDate, string studentId)
        {
            var query = (from session in context.Sessions
                         join attendance in context.Attendances
                         on session.SessionId equals attendance.SessionId
                         join timeSlot in context.TimeSlots
                         on session.TimeSlotId equals timeSlot.TimeSlotId
                         join room in context.Rooms
                         on session.RoomId equals room.RoomId
                         join groups in context.Groups
                         on session.GroupId equals groups.GroupId
                         join teacher in context.Teachers
                         on session.TeacherId equals teacher.TeacherId
                         where attendance.StudentId == studentId && session.Date >= startDate && session.Date <= endDate
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             AttendanceId = attendance.AttendanceId,
                             Date = session.Date,
                             Index = session.Index,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             GroupName = groups.GroupName,
                             Present = attendance.Present
                         }).ToList();
            return query;
        }

        public void UpdateAttandances(List<AttandanceDto> attandances)
        {
            throw new NotImplementedException();
        }
    }
}
