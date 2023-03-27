using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public void AddStudentGroups(List<StudentGroup> sg)
        {
            context.AddRange(sg);
            context.SaveChanges();
        }
    }
}
