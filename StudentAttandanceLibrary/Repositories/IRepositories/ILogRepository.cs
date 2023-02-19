using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.IRepositories
{
    public interface ILogRepository
    {
        public Account? Login(string email, string password);
        public bool ConfirmEmail(string email);
        public bool ChangePassword(string email, string oldPassword, string newPassword);
        public void Logout();
    }
}
