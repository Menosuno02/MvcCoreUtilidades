using System.Diagnostics.Eventing.Reader;

namespace MvcCoreUtilidades.Helpers
{
    // Aqui deberíamos tener todas las carpetas que
    // deseemos que nuestros controllers utilicen
    public enum Folders
    { Images = 0, Facturas = 1, Uploads = 2, Temporal = 3 }
    public class HelperPathProvider
    {
        // Necesitamos acceder al sistema de archivos del Web Server (wwwroot)

        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
    }
}
