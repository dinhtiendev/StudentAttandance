using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ITimeSlotRepository
    {
        public IQueryable<TimeSlot> GetTimeSlots();
    }
}
