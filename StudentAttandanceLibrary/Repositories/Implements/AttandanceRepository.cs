﻿using StudentAttandanceLibrary.Repositories.IRepositories;
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

        public AttandanceDto GetAttandanceById(int sessionId, string studentId)
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
                         where attendance.SessionId == sessionId && attendance.StudentId == studentId
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
            if (query == null)
            {
                query = (from session in context.Sessions
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
                         join student in context.StudentGroups
                         on groups.GroupId equals student.GroupId
                         where session.SessionId == sessionId && student.StudentId == studentId
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
                         }).FirstOrDefault();
            }
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

        public List<AttandanceDto> GetAttandancesBySesionId(int sesionId)
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
                         join student in context.Students
                         on attendance.StudentId equals student.StudentId
                         where session.SessionId == sesionId
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             AttendanceId = attendance.AttendanceId,
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             Student = new Student { StudentId = student.StudentId, FullName = student.FullName, UserName = student.UserName },
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId },
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName },
                             Present = attendance.Present
                         }).ToList();
            return query;
        }

        public List<AttandanceDto> NewAttandancesBySesionId(int sesionId)
        {
            var oldSession = context.Sessions.FirstOrDefault(x => x.SessionId == sesionId);
            oldSession.Attanded = true;
            //Get list students in groupId of this session
            var listAttandances = (from session in context.Sessions
                                join student in context.StudentGroups
                                on session.GroupId equals student.GroupId
                                where session.SessionId == sesionId
                                select new Attendance
                                {
                                    SessionId = session.SessionId,
                                    StudentId = student.StudentId,
                                    Present = true,
                                    Description = ""
                                }).ToList();
            context.Attendances.AddRange(listAttandances);
            context.SaveChanges();
            //Get list attandances
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
                         join student in context.Students
                         on attendance.StudentId equals student.StudentId
                         where session.SessionId == sesionId
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             AttendanceId = attendance.AttendanceId,
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             Student = new Student { StudentId = student.StudentId, FullName = student.FullName, UserName = student.UserName },
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId },
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName },
                             Present = attendance.Present
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
                             Student = new Student { StudentId = studentId },
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
            var query1 = (from session in context.Sessions
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
                         join student in context.StudentGroups
                         on groups.GroupId equals student.GroupId
                         where student.StudentId == studentId && session.Date >= startDate && session.Date <= endDate && session.Attanded == false
                         select new AttandanceDto
                         {
                             SessionId = session.SessionId,
                             Student = new Student { StudentId = studentId },
                             Date = session.Date,
                             Index = session.Index,
                             RoomName = room.RoomName,
                             Attanded = session.Attanded,
                             TeacherName = teacher.UserName,
                             TimeSlot = new TimeSlot { TimeSlotId = timeSlot.TimeSlotId, Description = timeSlot.Description },
                             Group = new Group { GroupName = groups.GroupName, GroupId = groups.GroupId },
                             Course = new Course { CourseId = course.CourseId, CourseCode = course.CourseCode, CourseName = course.CourseName }
                         }).ToList();
            query.AddRange(query1);
            return query;
        }

        public void UpdateAttandances(Dictionary<int, bool> attandances)
        {
            foreach (var attand in attandances)
            {
                var attandance = context.Attendances.FirstOrDefault(x => x.AttendanceId == attand.Key);
                attandance.Present = attand.Value;
                context.Attendances.Update(attandance);
            }
            context.SaveChanges();
        }
    }
}
