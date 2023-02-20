using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.ModelDtos;
using TeacherAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class TeacherRepository : ITeacherRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddTeacher(TeacherDto teacher)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeacher(TeacherDto teacher)
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
    }
}
