using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ICourseRepository
    {
        public List<Course> GetListCourses();
        public void AddCourse(Course course);
        public void UpdateCourse(Course course);
        public void DeleteCourse(Course course);
        public IQueryable<Course> GetAllCourses();
        public IQueryable<CourseDto> GetCoursesByCondition(string studentId, int termId);
    }
}
