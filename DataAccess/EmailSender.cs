using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmailSender
    {
        private readonly string fromEmail = "swpacckhoi@gmail.com";
        private readonly string fromPassword = "Swpacc1!";
        //port server cua gmail
        private readonly int port = 587;
        public bool sendEmail(string subject, string body, params string[] receipients)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    foreach (string receipient in receipients)
                    {
                        mail.To.Add(receipient);
                    }
                    mail.Subject = subject;
                    mail.Body = body;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", port))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                };
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Mail");
            string mail = Console.ReadLine();
            Console.WriteLine("Enter subject:");
            string subject = Console.ReadLine();
            Console.WriteLine("Enter body");
            string body = Console.ReadLine();

            EmailSender emailSender = new EmailSender();
            emailSender.sendEmail(subject, body, mail);
            Console.WriteLine("Sent");
        }
    }

}
