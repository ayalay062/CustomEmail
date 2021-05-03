using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace Repositories
{
   public class SendEmailRepository:FilterRepository, ISendEmailRepository
    {
        public bool send(dynamic mail)
        {
            
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail.mailAddress.ToString(), mail.password.ToString()),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail.mailAddress.ToString(), mail.name.ToString()),
                Subject = mail.subject.ToString(),
                Body = mail.message.ToString(),
                IsBodyHtml = true,
            };
            for (int i = 0; i < dsMail.Tables[0].Rows.Count; i++)
            {
                mailMessage.To.Add(dsMail.Tables[0].Rows[i][0].ToString());
                smtpClient.Send(mailMessage);
            }
            //mailMessage.To.Add("ch0556777165@gmail.com");

            //smtpClient.Send(mailMessage);
            return true;

        }
    }
}
