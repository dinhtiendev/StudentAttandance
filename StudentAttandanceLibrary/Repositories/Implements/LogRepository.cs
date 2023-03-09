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

        public IQueryable<object> GetUserInformation(int? roleId)
        {
            if (roleId == 1)
            {
                var query1 = from a in context.Accounts
                            join u in context.Admins
                            on a.AccountId equals u.AdminId
                            select new Admin
                            {
                                AdminId = u.AdminId,
                                UserName = u.UserName,
                                FullName = u.FullName,
                                Image = u.Image,
                            };
                return query1;
            }
            if (roleId == 2)
            {
                var query2 = from a in context.Accounts
                            join u in context.Teachers
                            on a.AccountId equals u.TeacherId
                            select new Teacher
                            {
                                TeacherId = u.TeacherId,
                                Image = u.Image,
                                Gender = u.Gender,
                                FullName = u.FullName,
                                Dob = u.Dob,
                                UserName = u.UserName,
                            };
                return query2;
            }
            var query3 = from a in context.Accounts
                        join u in context.Students
                        on a.AccountId equals u.StudentId
                        select new Student
                        {
                            StudentId = u.StudentId,
                            FullName = u.FullName,
                            UserName = u.UserName,
                            Dob = u.Dob,
                            Address = u.Address,
                            Gender = u.Gender,
                            Image = u.Image,
                        };
            return query3;
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
