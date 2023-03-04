﻿using StudentAttandanceLibrary.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IAttandanceRepository
    {
        public List<AttandanceDto> GetAttandancesByWeekAndStudentId(DateTime startDate, DateTime endDate, String StudentId);
        public List<AttandanceDto> GetAttandancesByWeekAndTeacherId(DateTime startDate, DateTime endDate, String TeacherId);
        public AttandanceDto GetAttandanceById(int id);
        public void UpdateAttandances(List<AttandanceDto> attandances);
    }
}
