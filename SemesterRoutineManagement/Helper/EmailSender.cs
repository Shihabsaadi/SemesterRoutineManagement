using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace SemesterRoutineManagement.Helper
{
    public static class EmailSender
    {
        public static void SendEmail(string recipient, string subject, string body)
        {
            string senderEmail = "sba1320@gmail.com"; // Replace with your sender email address
            string senderPassword = "xdyczimfbibaalbp"; // Replace with your sender email password

            MailMessage mail = new MailMessage(senderEmail, recipient, subject, body);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server details
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);

            try
            {
                client.Send(mail);
                // Email sent successfully
            }
            catch (SmtpException ex)
            {
                // Handle exception
            }
        }
    }
}