using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.ModelDtos;

namespace TeacherAttandanceLibrary.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        public TeacherDto GetTeacherById(String id);
        public List<TeacherDto> GetListTeachers();
        public void AddTeacher(TeacherDto teacher);
        public void UpdateTeacher(TeacherDto teacher);
        public void DeleteTeacher(TeacherDto teacher);
        public IQueryable<TeacherDto> GetAllTeachers();

    }
}
