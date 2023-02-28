using StudentAttandanceLibrary.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.Repositories.Implements
{
    public class MailRepository : IMailRepository
    {
        public string sendNewPassword(String sender)
        {
            var password = RandomString(10);
            // Create a new MailMessage object
            MailMessage message = new MailMessage();
            message.From = new MailAddress("tiendvhe153729@fpt.edu.vn");
            message.To.Add(new MailAddress(sender));
            message.Subject = "New Password";
            
            message.Body = "This is your new password: " + password;
            // Create a new SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("tiendvhe153729@fpt.edu.vn", "tienhocngu");
            smtpClient.EnableSsl = true;

            // Send the email message
            smtpClient.Send(message);
            return password;
        }
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
