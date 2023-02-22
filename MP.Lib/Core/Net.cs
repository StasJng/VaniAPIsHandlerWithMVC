using System.Net;
using System.Net.Mail;
using System.Text;
using MP.Lib.Models;

namespace MP.Lib.Core
{
    public class Net
    {
        public static WebConfiguration Configuration
        {
            get { return WebConfiguration.Instant; }
        }
        #region Send Mail
        public static void SendMail(Email email)
        {
            SendMail(email.From, email.To, email.Subject, email.Body);
        }
        public static void SendMail(string subject, string body)
        {
            SendMail(Configuration.SmtpSender, Configuration.AdminEmail, subject, body);
        }

        public static void SendMail(string to, string subject, string body)
        {
            SendMail(Configuration.SmtpSender, to, subject, body);
        }

        public static void SendMail(string from, string to, string subject, string body)
        {

            var _mail = new MailMessage(new MailAddress(from, Configuration.domain), new MailAddress(to))
            {
                Subject = string.Format("[{0}] - {1}", Configuration.domain, subject),
                Body = body + Configuration.EmailSignature,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };


            //SmtpClient smtpMail = new SmtpClient("smtp.gmail.com");
            var _smtpMail = new SmtpClient(Configuration.SmtpServer, Configuration.SmtpPort)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = Configuration.SmtpSsl
            };

            if (Configuration.SmtpCredentials)
            {
                _smtpMail.Credentials = new NetworkCredential(Configuration.SmtpUserName, Configuration.SmtpUserPass);
            }

            // and then send the mail
            _smtpMail.Send(_mail);
        }
        #endregion
    }
}
