using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class StudentRepository : IStudentRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddStudent(List<Account> listA, List<Student> listS)
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

        public void UpdateStudent(StudentDto student)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentDto> GetAllStudents()
        {
            var query = from student in context.Students
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
                        };
            return query;
        }

        public List<Student> GetStudents()
        {
            var data = context.Students.ToList();
            return data;
        }
    }
}
