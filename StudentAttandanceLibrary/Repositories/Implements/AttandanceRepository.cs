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
                         join course in context.Courses
                         on groups.CourseId equals course.CourseId
                         where attendance.AttendanceId == id
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             AttendanceId = attendance.AttendanceId,
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId } ,
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName },
                             Present = attendance.Present
                         }).FirstOrDefault();
            return query;
        }

        public List<AttandanceDto> GetAttandancesByWeekAndTeacherId(DateTime startDate, DateTime endDate, string teacherId)
        {
            var query = (from session in context.Sessions
                         join timeSlot in context.TimeSlots
                         on session.TimeSlotId equals timeSlot.TimeSlotId
                         join room in context.Rooms
                         on session.RoomId equals room.RoomId
                         join groups in context.Groups
                         on session.GroupId equals groups.GroupId
                         join teacher in context.Teachers
                         on session.TeacherId equals teacher.TeacherId
                         join course in context.Courses
                         on groups.CourseId equals course.CourseId
                         where session.TeacherId == teacherId && session.Date >= startDate && session.Date <= endDate
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId },
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName }
                         }).ToList();
            return query;
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
                         join course in context.Courses
                         on groups.CourseId equals course.CourseId
                         where attendance.StudentId == studentId && session.Date >= startDate && session.Date <= endDate
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             AttendanceId = attendance.AttendanceId,
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId },
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName },
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
