namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFiles
    {
        private HelperPathProvider helperPathProvider;

        public HelperUploadFiles(HelperPathProvider helperPathProvider)
        {
            this.helperPathProvider = helperPathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string fileName = file.FileName;
            // Recuperamos la ruta de nuestro fichero
            string path =
                this.helperPathProvider.MapPath(fileName, Folders.Mails);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
