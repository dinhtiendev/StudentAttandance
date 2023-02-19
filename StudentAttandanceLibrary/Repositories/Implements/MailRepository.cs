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
        public void sendNewPassword(String sender)
        {
            string to = "tiendvhe153729@fpt.edu.vn";
            string from = sender;
            string subject = "New Password";
            string body = "This is your new password: " + RandomString(10);

            MailMessage message = new MailMessage(from, to, subject, body);

            SmtpClient client = new SmtpClient("smtp.example.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("tiendvhe153729@fpt.edu.vn", "tien261001");
            client.EnableSsl = true;

            client.Send(message);
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
