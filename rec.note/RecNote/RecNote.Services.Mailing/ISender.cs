using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace RecNote.Services.Mailing
{
    /// <summary>
    /// Interface de envió de correos
    /// </summary>
    public interface ISender
    {
        void Send(MailAddress from, MailAddress to, string subject, ContentType type, string message);
    }
}
