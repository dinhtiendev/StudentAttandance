using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        public TeacherDto GetTeacherById(String id);
        public TeacherDto? GetTeacherByEmail(String email);
        public void AddTeacher(List<Account> listA, List<Teacher> listT);
        public void UpdateTeacher(TeacherDto teacher);
        public void DeleteTeacher(string id);

        public IQueryable<TeacherDto> GetAllTeachers();
        public List<Teacher> GetTeachers();
    }
}
