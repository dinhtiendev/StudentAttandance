using StudentAttandanceLibrary.Models;
using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class LogRepository : ILogRepository
    {
        StudentAttendanceManagementContext context;
        public LogRepository()
        {
            context = new StudentAttendanceManagementContext();
        }

        public bool ChangePassword(string email, string newPassword)
        {
            var acc = context.Accounts.FirstOrDefault(x => x.Email == email);
            if (acc != null)
            {
                acc.Password = newPassword;
                context.Accounts.Update(acc);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            var acc = context.Accounts.FirstOrDefault(x => x.Email == email);
            if (acc != null)
            {
                if (acc.Password.CompareTo(oldPassword) == 0)
                {
                    acc.Password = newPassword;
                    context.Accounts.Update(acc);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool ConfirmEmail(string email)
        {
            return context.Accounts.Any(a => a.Email == email);
        }

        public Account? Login(string email, string password)
        {
            return context.Accounts.FirstOrDefault(
                x => x.Email.CompareTo(email) == 0
                && x.Password.CompareTo(password) == 0
                && x.Status == true);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
