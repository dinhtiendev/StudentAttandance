using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Repositories.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class StudentRepository : IStudentRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddStudent(StudentDto student)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(StudentDto student)
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

        public List<StudentDto> GetListStudents()
        {
            throw new NotImplementedException();
        }

        public TeacherDto GetTeacherById(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(StudentDto student)
        {
            throw new NotImplementedException();
        }
    }
}
