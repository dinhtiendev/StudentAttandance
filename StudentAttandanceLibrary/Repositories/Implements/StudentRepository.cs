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
        public void AddStudent(StudentDto student)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(StudentDto student)
        {
            throw new NotImplementedException();
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
