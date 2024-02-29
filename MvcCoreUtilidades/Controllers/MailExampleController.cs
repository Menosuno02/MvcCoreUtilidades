using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailExampleController : Controller
    {
        private HelperMails helperMails;

        public MailExampleController
            (HelperMails helperMails)
        {
            this.helperMails = helperMails;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string para, string asunto, string mensaje,
            IFormFile file)
        {
            await this.helperMails.SendMail(para, asunto, mensaje, file);
            ViewData["MENSAJE"] = "Email enviado";
            return View();
        }
    }
}
