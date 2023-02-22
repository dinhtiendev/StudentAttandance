using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        public StudentDto? GetStudentByEmail(String id);
        public List<StudentDto> GetListStudents();
        public void AddStudent(List<Account> listA,List<Student> listS);
        public void UpdateStudent(StudentDto student);
        public void DeleteStudent(string id);
        public IQueryable<StudentDto> GetAllStudents();
        public List<Student> GetStudents();
    }
}
