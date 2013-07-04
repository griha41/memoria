using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace RecNote.Services.Mailing.Gmail
{
    public class Sender : ISender
    {
        string User { get; set; }
        string DisplayUser { get; set; }
        string Password { get; set; }
        bool EnableSsl { get; set; }
        int Port { get; set; }
        string Host { get; set; }
        SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }



        public void Send(MailAddress from, MailAddress to, string subject, ContentType type, string message)
        {
            using (var client = new SmtpClient(this.Host, this.Port))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = this.EnableSsl;
                client.Credentials = new System.Net.NetworkCredential(this.User, this.Password);

                var mail = new MailMessage(new MailAddress(this.User, this.DisplayUser), to)
                {
                    Subject = subject,
                    ReplyTo = from,
                    BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1"),
                    Body = message,
                    IsBodyHtml = (type == ContentType.Html)
                };
                client.Send(mail);
            }
            
        }
    }
}
