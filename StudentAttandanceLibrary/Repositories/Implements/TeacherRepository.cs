using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Repositories.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class TeacherRepository : ITeacherRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddTeacher(List<Account> listA, List<Teacher> listT)
        {
            context.Accounts.AddRange(listA);
            context.Teachers.AddRange(listT);
            context.SaveChanges();
        }

        public void DeleteTeacher(string id)
        {
            var account = context.Accounts.Where(x => x.AccountId.Equals(id)).FirstOrDefault();
            account.Status = false;
            context.SaveChanges();
        }

        public List<TeacherDto> GetListTeachers()
        {
            throw new NotImplementedException();
        }

        public TeacherDto GetTeacherById(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTeacher(TeacherDto teacher)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TeacherDto> GetAllTeachers()
        {
            var query = from teacher in context.Teachers
                        join account in context.Accounts
                        on teacher.TeacherId equals account.AccountId
                        where account.Status == true
                        select new TeacherDto
                        {
                            TeacherId = teacher.TeacherId,
                            FullName = teacher.FullName,
                            UserName = teacher.FullName,
                            Email = account.Email,
                            Image = teacher.Image,
                            Dob = teacher.Dob,
                            Gender = teacher.Gender,
                            RoleId = account.RoleId,
                            Status = account.Status,
                        };
            return query;
        }

        public List<Teacher> GetTeachers()
        {
            var data = context.Teachers.ToList();
            return data;
        }
    }
}
