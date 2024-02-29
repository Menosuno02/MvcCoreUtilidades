using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPathProvider;
        private HelperUploadFiles helperUploadFiles;

        public UploadFilesController
            (HelperPathProvider helperPathProvider, HelperUploadFiles helperUploadFiles)
        {
            this.helperPathProvider = helperPathProvider;
            this.helperUploadFiles = helperUploadFiles;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {

            string path = await helperUploadFiles.UploadFileAsync(fichero, Folders.Uploads);
            ViewData["URL"] = this.helperPathProvider.MapUrlPath(fichero.FileName, Folders.Uploads); ;
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
