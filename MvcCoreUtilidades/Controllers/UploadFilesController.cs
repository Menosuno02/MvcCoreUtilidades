using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            // Aqui trabajaremos con Files, System.IO
            // Comenzamos almacenando el fichero en una ruta temporal
            string tempFolder = Path.GetTempPath();
            string fileName = fichero.FileName;
            // Necesitamos la ruta física para escribir el fichero
            // La ruta es la combinación de tempFolder y fileName
            // C:\Documents\Temp\file1.txt
            // /var/documents/temp/file1.txt
            // Cuando estemos hablando de files (System.IO) para
            // acceder a rutas, siempre debemos utilizar Path.Combine
            string path = Path.Combine(tempFolder, fileName);

            return View();
        }
    }
}
