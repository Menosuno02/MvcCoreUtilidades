using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPathProvider;

        public UploadFilesController(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string path = this.helperPathProvider.MapPath(fichero.FileName, Folders.Uploads);
            // Subimos el fichero utilizando Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                // Mediante IFormFile copiamos el contenido del fichero al stream
                await fichero.CopyToAsync(stream);
            }
            ViewData["URL"] = this.helperPathProvider.MapUrlPath(fichero.FileName, Folders.Uploads); ;
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
