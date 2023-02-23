using StudentAttandanceLibrary.Models;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IGroupRepository
    {
        public IQueryable<Group> GetGroupsByTermAndCourse(int termId, int courseId);
    }
}
