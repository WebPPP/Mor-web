using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Site
{
    public class EmailService
    {
        SendGridMessage mail = new SendGridMessage();

        public EmailService()
        {
            mail.From = new System.Net.Mail.MailAddress("ben.bs52@gmail.com", "Ben Brandes");
            mail.AddTo("ben.bs52@gmail.com");
            mail.Subject = "test";
            mail.Text = "Test Succeed!";
        }

        public void SendEmail()
        {
            var transportWeb = new Web("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");
            transportWeb.DeliverAsync(mail);
        }
    }
}