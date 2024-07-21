using ETicaretProje.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace ETicaretProje.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(OrderFormViewModel model)
        {
            SmtpClient smtpClient = new SmtpClient("mail.yandex.com",587);
            smtpClient.Credentials = new NetworkCredential("emailkullanıcıad","şifre");
            smtpClient.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("emailfromkullanıcıad");
            message.To.Add("emailtokullanıcıad");
            message.Subject = "Siteden mesaj geldi";
            message.Body = $"Mail bilgileri <hr/> Ad Soyad : {model.User.Name} {model.User.Name} <hr/> ";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
