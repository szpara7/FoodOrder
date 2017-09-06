using FoodOrder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace FoodOrder.Models
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string recipient, string title, string message)
        {
            string smtpHost = "smtp.wp.pl";
            string fromEmail = "foodorder@wp.pl";
            string emailPassword = "1qaz2wsx3edc";

            MailAddress fromAddress = new MailAddress(fromEmail);
            MailAddress toAddress = new MailAddress(recipient);

            MailMessage mailMessage = new MailMessage(fromEmail, recipient)
            {
                Body = message,
                Subject = title
            };

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = smtpHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, emailPassword),
                Timeout = 180000
            };

            smtpClient.Send(mailMessage);
        }
    }
}