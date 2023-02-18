using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ICourseRepository
    {
        public List<Course> GetListCourses();
        public void AddCourse(Course course);
        public void UpdateCourse(Course course);
        public void DeleteCourse(Course course);
    }
}
