using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class GroupRepository : IGroupRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
            context.SaveChanges();
        }

        public void AddGroups(List<Group> groups)
        {
            context.Groups.AddRange(groups);
            context.SaveChanges();
        }

        public void DeleteGroup(int id)
        {
            var g = context.Groups.Where(x => x.GroupId == id).FirstOrDefault();

            if (g != null)
            {
                var sg = context.StudentGroups.Where(x => x.GroupId == id).ToList();
                var ss = context.Sessions.Where(x => x.GroupId == id).ToList();
                context.StudentGroups.RemoveRange(sg);
                context.Sessions.RemoveRange(ss);
                context.Groups.Remove(g);
                context.SaveChanges();
            }
        }

        public IQueryable<GroupDto> FilterGroups(int termId, int courseId)
        {
            var query = from g in context.Groups
                        join t in context.Teachers on g.TeacherId equals t.TeacherId
                        join gs in context.StudentGroups on g.GroupId equals gs.GroupId
                        join s in context.Sessions on g.GroupId equals s.SessionId
                        where g.TermId == termId && g.CourseId == courseId
                        group s by new { g.GroupId, g.GroupName, t.UserName } into groupResult
                        select new GroupDto
                        {
                            GroupId = groupResult.Key.GroupId,
                            GroupName = groupResult.Key.GroupName,
                            Teacher = groupResult.Key.UserName,
                            NumberSlots = groupResult.Count(),
                            TotalStudents = context.StudentGroups.Count(x => x.GroupId == groupResult.Key.GroupId)
                        };
            //var query = (from g in context.Groups
            //            join t in context.Teachers
            //            on g.TeacherId equals t.TeacherId
            //            join s in context.Sessions
            //            on g.GroupId equals s.GroupId
            //            join st in context.StudentGroups
            //            on g.GroupId equals st.GroupId
            //            //join r in context.Rooms
            //            //on s.RoomId equals r.RoomId
            //            where g.TermId == termId && g.CourseId == courseId
            //            select new GroupDto
            //            {
            //                GroupId = g.GroupId,
            //                GroupName = g.GroupName,
            //                Teacher = t.TeacherId,
            //                //Room = r.RoomName,
            //                NumberSlots = context.Sessions.Where(x => x.GroupId == g.GroupId).Count(),
            //                TotalStudents = context.Sessions.Where(x => x.GroupId == g.GroupId).Count(),
            //            }).Distinct();
            return query;
        }

        public Group GetGroup(int id)
        {
            return context.Groups.Where(x => x.GroupId == id).FirstOrDefault();
        }

        public IQueryable<Group> GetGroupsByConditions(string className, int termId, int courseId)
        {
            var query = context.Groups.Where(x => x.GroupName.Equals(className) && x.TermId == termId && x.CourseId == courseId);
            return query;
        }

        public IQueryable<Group> GetGroupsByTermAndCourse(int termId, int courseId)
        {
            var query = context.Groups.Where(x => x.TermId == termId && x.CourseId == courseId);
            return query;
        }
    }
}
