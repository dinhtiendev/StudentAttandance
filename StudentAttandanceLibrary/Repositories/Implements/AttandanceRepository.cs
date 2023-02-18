using StudentAttandanceLibrary.Repositories.IRepositories;
using StudentAttandanceLibrary.Repositories.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class AttandanceRepository : IAttandanceRepository
    {
        public AttandanceDto GetAttandanceById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AttandanceDto> GetAttandancesBySlotIdAndGroupId(int slotId, int groupId)
        {
            throw new NotImplementedException();
        }

        public List<AttandanceDto> GetAttandancesByWeekAndStudentId(DateTime startDate, DateTime endDate, string StudentId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAttandances(List<AttandanceDto> attandances)
        {
            throw new NotImplementedException();
        }
    }
}
