using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ISessionRepository
    {
        public IQueryable<SessionDto> GetAllSessionsByCondition(string studentId, int termId, int courseId);

        public void AddSessions(List<Session> sessions);
    }
}
