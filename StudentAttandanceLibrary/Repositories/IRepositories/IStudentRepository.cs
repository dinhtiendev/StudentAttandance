using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        public TeacherDto GetTeacherById(String id);
        public List<StudentDto> GetListStudents();
        public void AddStudents(List<Account> listA,List<Student> listS);
        public void UpdateStudent(StudentDto student);
        public void DeleteStudent(string id);
        public IQueryable<StudentDto> GetAllStudents();
        public List<Student> GetStudents();
        public IQueryable<Student> GetStudentsByConditions(int termId, int courseId, int groupId);
    }
}
