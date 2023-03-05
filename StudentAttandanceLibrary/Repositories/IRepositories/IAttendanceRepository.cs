using StudentAttandanceLibrary.ModelDtos;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IAttendanceRepository
    {
        public IQueryable<AttendanceDto> GetAttendancesByCondition(string studentId, int termId, int courseId);
    }
}
