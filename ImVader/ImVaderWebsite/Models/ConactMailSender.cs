using System;

namespace ImVaderWebsite.Models
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;

    public class ContactMailSender
    {
        private readonly string recieverMail;
        private readonly string senderMail;
        private readonly string senderPassWord;
        private readonly string host;

        public ContactMailSender()
        {
            recieverMail = ConfigurationManager.AppSettings["recieverMail"];
            senderMail = ConfigurationManager.AppSettings["senderMail"];
            senderPassWord = ConfigurationManager.AppSettings["senderPassWord"];

            host = ConfigurationManager.AppSettings["host"];
        }

        public void Send(ContactForm form)
        {
            SmtpClient smtpClient = null;

            MailMessage message = null;
            try
            {
                message = new MailMessage(new MailAddress(senderMail), new MailAddress(recieverMail))
                {
                    Subject = form.Title,
                    Body = form.Message + String.Format("\nFrom:{0} \nemail:{1} \nUser phone: {2}", form.Name, form.Email, form.Phone)
                };
                smtpClient = new SmtpClient(host, Convert.ToInt32(ConfigurationManager.AppSettings["port"]))
                {
                    Credentials = new NetworkCredential(senderMail, senderPassWord)
                };
                smtpClient.EnableSsl = true;
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