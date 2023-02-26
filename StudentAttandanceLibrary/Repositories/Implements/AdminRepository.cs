using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class AdminRepository : IAdminRepository
    {
        StudentAttendanceManagementContext context;
        public AdminRepository()
        {
            context = new StudentAttendanceManagementContext();
        }
        public AdminDto? GetAdminByEmail(string email)
        {
            return context.Admins.ToList()
                .Join(context.Accounts.Where(x => x.Email.CompareTo(email) == 0), 
                admin => admin.AdminId,
                account => account.AccountId,
                (admin, account) => new
                {
                    AdminId = admin.AdminId,
                    FullName = admin.FullName,
                    UserName = admin.UserName,
                    Image = admin.Image,
                    Email = account.Email,
                    RoleId = account.RoleId,
                    Status = account.Status,
                }).Select(joined => new AdminDto
                {
                    AdminId = joined.AdminId,
                    FullName = joined.FullName,
                    UserName = joined.UserName,
                    Image = joined.Image,
                    Email = joined.Email,
                    RoleId = joined.RoleId,
                    Status = joined.Status,
                }).FirstOrDefault(x => x.Email.CompareTo(email) == 0);
        }
    }
}
