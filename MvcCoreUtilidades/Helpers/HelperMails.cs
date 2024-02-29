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

        private async Task<MailMessage> ConfigureMailMessage
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
                string path =
                    await this.helperUploadFiles.UploadFileAsync(file, Folders.Mails);
                Attachment attachment = new Attachment(path);
                mail.Attachments.Add(attachment);
            }
            return mail;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            string user = this.configuration.GetValue<string>
                ("MailSettings:Credentials:User");
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
            return smtpClient;
        }

        public async Task SendMailAsync
            (string para, string asunto, string mensaje, IFormFile file)
        {
            MailMessage mail = await ConfigureMailMessage(para, asunto, mensaje, file);
            SmtpClient smtpClient = ConfigureSmtpClient();
            await smtpClient.SendMailAsync(mail);
        }
    }
}
