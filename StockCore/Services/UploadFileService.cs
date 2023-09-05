using StockCore.Interfaces;

namespace StockCore.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public UploadFileService(IWebHostEnvironment webHostEnvironment , IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public bool IsUpload(List<IFormFile> fromFiles)
        {
            //throw new NotImplementedException();
            return fromFiles != null && fromFiles.Sum(f => f.Length) > 0;
        }


        public string Validation(List<IFormFile> fromFiles)
        {
            //throw new NotImplementedException();
            foreach(var fromFile in fromFiles)
            {
                if(!ValicationExtension(fromFile.FileName))
                {
                    return "Invalid file extension";
                }
                if (!ValidationSize(fromFile.Length))
                {
                    return "The file is too large";
                }

            }
            return null;
        }
        public async Task<List<string>> UploadImages(List<IFormFile> fromFiles)
        {
            //throw new NotImplementedException();
            List<string> listFileName = new List<string> ();
            string uploadPath = $"{webHostEnvironment.WebRootPath}/images/";
            foreach (var file in fromFiles) { 
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string fullPath = uploadPath + fileName;
                using (var stream = File.Create(fullPath))
                {
                    await file.CopyToAsync(stream);
                }
                listFileName.Add(fileName);
            }
            return listFileName;
        }

        public bool ValicationExtension(string fileName)
        {
            string[] permittedExtension = { ".jpg", ".png" };
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (String.IsNullOrEmpty(ext) || !permittedExtension.Contains(ext))
            {
                return false;
            }
            return true;

        }

        public bool ValidationSize(long fileSize)
        {
            return configuration.GetValue<long>("FileSizeLimit") > fileSize;
        }

    }
}
