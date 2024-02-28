using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private IWebHostEnvironment hostEnvironment;

        public UploadFilesController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            // Ruta de nuestro server
            string rootFolder = this.hostEnvironment.WebRootPath;
            string fileName = fichero.FileName;
            // Necesitamos la ruta física para escribir el fichero
            // La ruta es la combinación de tempFolder y fileName
            // C:\Documents\Temp\file1.txt
            // /var/documents/temp/file1.txt
            // Cuando estemos hablando de files (System.IO) para
            // acceder a rutas, siempre debemos utilizar Path.Combine
            string path = Path.Combine(rootFolder, "uploads", fileName);
            // Subimos el fichero utilizando Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                // Mediante IFormFile copiamos el contenido del fichero al stream
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
