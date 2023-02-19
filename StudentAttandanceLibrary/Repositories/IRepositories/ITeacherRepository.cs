using StudentAttandanceLibrary.Repositories.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        public TeacherDto GetTeacherById(String id);
        public List<TeacherDto> GetListTeachers();
        public void AddTeacher(TeacherDto teacher);
        public void UpdateTeacher(TeacherDto teacher);
        public void DeleteTeacher(TeacherDto teacher);
    }
}
