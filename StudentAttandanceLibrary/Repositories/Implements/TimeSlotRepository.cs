using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public IQueryable<TimeSlot> GetTimeSlots()
        {
           return context.TimeSlots;
        }
    }
}
