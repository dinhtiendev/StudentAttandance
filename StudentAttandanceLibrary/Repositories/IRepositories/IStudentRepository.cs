using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        public TeacherDto GetTeacherById(String id);
        public List<StudentDto> GetListStudents();
        public void AddStudent(StudentDto student);
        public void UpdateStudent(StudentDto student);
        public void DeleteStudent(StudentDto student);
    }
}
