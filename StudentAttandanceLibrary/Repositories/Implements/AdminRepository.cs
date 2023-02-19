using StudentAttandanceLibrary.ModelDtos;
using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }) as AdminDto;
        }
    }
}
