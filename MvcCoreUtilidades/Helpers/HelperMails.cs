using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Helpers
{

    public class HelperMails
    {
        private IConfiguration configuration;
        private HelperUploadFiles helperUploadFiles;

        public HelperMails
            (IConfiguration configuration,
            HelperUploadFiles helperUploadFiles)
        {
            this.configuration = configuration;
            this.helperUploadFiles = helperUploadFiles;
        }

        public async Task SendMail
            (string para, string asunto, string mensaje, IFormFile file)
        {
            MailMessage mail = new MailMessage();
            // Necesitamos indicar From, desde donde se envian los mails
            string user = this.configuration.GetValue<string>
                ("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);
            mail.To.Add(para);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            // Si tiene attachment
            if (file != null)
            {
                string path = await this.helperUploadFiles.UploadFileAsync(file);
                Attachment attachment = new Attachment(path);
                mail.Attachments.Add(attachment);
            }

            string password = this.configuration.GetValue<string>
                ("MailSettings:Credentials:Password");
            string hostName = this.configuration.GetValue<string>
                ("MailSettings:ServerSmtp:Host");
            int port = this.configuration.GetValue<int>
                ("MailSettings:ServerSmtp:Port");
            bool enableSsl = this.configuration.GetValue<bool>
                ("MailSettings:ServerSmtp:EnableSsl");
            bool defaultCredentials = this.configuration.GetValue<bool>
                ("MailSettings:ServerSmtp:DefaultCredentials");
            // Creamos el servidor SMTP para enviar los mails
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = hostName;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;
            smtpClient.UseDefaultCredentials = defaultCredentials;
            // Creamos las credenciales de red para enviar el mail
            NetworkCredential credentials = new NetworkCredential(user, password);
            smtpClient.Credentials = credentials;
            await smtpClient.SendMailAsync(mail);
        }
    }
}
