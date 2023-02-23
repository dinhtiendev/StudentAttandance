using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class CourseRepository : ICourseRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> GetAllCourses()
        {
            var query = context.Courses;
            return query;
        }

        public List<Models.Course> GetListCourses()
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Models.Course course)
        {
            throw new NotImplementedException();
        }
    }
}
