using System;

namespace ImVaderWebsite.Models
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;

    public class ContactMailSender
    {
        private readonly string siteMail;
        private readonly string passWord;
        private readonly string host;

        public ContactMailSender()
        {
            siteMail = ConfigurationManager.AppSettings["SiteMail"];
            passWord = ConfigurationManager.AppSettings["EmailPassword"];
            host = ConfigurationManager.AppSettings["host"];
        }

        public void Send(ContactForm form)
        {
            SmtpClient smtpClient = null;

            MailMessage message = null;
            try
            {
                message = new MailMessage(new MailAddress(form.Email), new MailAddress(siteMail))
                {
                    Subject = form.Title,
                    Body = form.Message + "\n User phone: " + form.Phone
                };
                smtpClient = new SmtpClient(host, Convert.ToInt32(ConfigurationManager.AppSettings["port"]))
                {
                    Credentials = new NetworkCredential(siteMail, passWord)
                };
                smtpClient.Send(message);
            }
            catch (SmtpException)
            {
                if (message != null)
                    message.Dispose();
                if (smtpClient != null)
                    smtpClient.Dispose();
                throw;
            }
        }
    }
}