using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class StudentRepository : IStudentRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddStudents(List<Account> listA, List<Student> listS)
        {
            context.Accounts.AddRange(listA);
            context.Students.AddRange(listS);
            context.SaveChanges();
        }

        public void DeleteStudent(string id)
        {
            var account = context.Accounts.Where(x => x.AccountId.Equals(id)).FirstOrDefault();
            account.Status = false;
            context.SaveChanges();
        }

        public List<StudentDto> GetListStudents()
        {
            throw new NotImplementedException();
        }

        public StudentDto? GetStudentByEmail(string email)
        {
            var query = (from student in context.Students
                         join account in context.Accounts
                         on student.StudentId equals account.AccountId
                         where account.Status == true
                         select new StudentDto
                         {
                             StudentId = student.StudentId,
                             FullName = student.FullName,
                             UserName = student.FullName,
                             Email = account.Email,
                             Image = student.Image,
                             Dob = student.Dob,
                             Gender = student.Gender,
                             Address = student.Address,
                             RoleId = account.RoleId,
                             Status = account.Status,
                         }).FirstOrDefault();
            return query;
        }

        public void RestoreStudent(string id)
        {
            var account = context.Accounts.Where(x => x.AccountId.Equals(id)).FirstOrDefault();
            account.Status = true;
            context.SaveChanges();
        }

        public IQueryable<StudentDto> GetAllStudents()
        {
            var query = from student in context.Students
                        join account in context.Accounts
                        on student.StudentId equals account.AccountId
                        //where account.Status == true
                        select new StudentDto
                        {
                            StudentId = student.StudentId,
                            FullName = student.FullName,
                            UserName = student.FullName,
                            Email = account.Email,
                            Image = student.Image,
                            Dob = student.Dob,
                            Gender = student.Gender,
                            Address = student.Address,
                            RoleId = account.RoleId,
                            Status = account.Status,
                        };
            return query;
        }

        public List<Student> GetStudents()
        {
            var data = context.Students.ToList();
            return data;
        }

        public IQueryable<Student> GetStudentsByConditions(int termId, int courseId, int groupId)
        {
            var query = from student in context.Students
                        join account in context.Accounts
                        on student.StudentId equals account.AccountId
                        join sg in context.StudentGroups
                        on student.StudentId equals sg.StudentId
                        join g in context.Groups
                        on sg.GroupId equals g.GroupId
                        join t in context.Terms
                        on g.TermId equals termId
                        join c in context.Courses
                        on g.CourseId equals courseId
                        where t.TermId == termId && c.CourseId == courseId && g.GroupId == groupId
                        && account.Status == true
                        select new Student
                        {
                            StudentId = student.StudentId,
                            FullName = student.FullName,
                            UserName = student.UserName,
                            Image = student.Image,
                            Dob = student.Dob,
                            Gender = student.Gender,
                            Address = student.Address,
                        };
            return query;
        }

        public IQueryable<StudentDto> GetStudentsByK(string k)
        {
            var query = from student in context.Students
                        join account in context.Accounts
                        on student.StudentId equals account.AccountId
                        where student.StudentId.Substring(2, 2).Contains(k.Substring(1, 2))
                                    //where account.Status == true
                                    select new StudentDto
                                    {
                                        StudentId = student.StudentId,
                                        FullName = student.FullName,
                                        UserName = student.FullName,
                                        Email = account.Email,
                                        Image = student.Image,
                                        Dob = student.Dob,
                                        Gender = student.Gender,
                                        Address = student.Address,
                                        RoleId = account.RoleId,
                                        Status = account.Status,
                                    };
            return query;
        }
    }
}
