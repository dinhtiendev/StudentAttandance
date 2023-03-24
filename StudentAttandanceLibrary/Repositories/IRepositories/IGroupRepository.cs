using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface IGroupRepository
    {
        public IQueryable<Group> GetGroupsByTermAndCourse(int termId, int courseId);

        public IQueryable<GroupDto> FilterGroups(int termId, int courseId);

        public IQueryable<Group> GetGroupsByConditions(string className, int termId, int courseId);

        public Group GetGroup(int id);

        public void AddGroup(Group group);

        public void AddGroups(List<Group> groups);

        public void DeleteGroup(int id);
    }
}
