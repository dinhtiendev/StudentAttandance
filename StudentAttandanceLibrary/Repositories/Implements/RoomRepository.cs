using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class RoomRepository : IRoomRepository
    {
        StudentAttendanceManagementContext context = new StudentAttendanceManagementContext();

        public IQueryable<Room> GetRooms()
        {
            return context.Rooms;
        }
    }
}
