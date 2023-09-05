namespace StockCore.Interfaces
{
    public interface IUploadFileService
    {
        bool IsUpload(List<IFormFile> fromFiles);
        string Validation(List<IFormFile> fromFile);
        Task<List<string>> UploadImages(List<IFormFile> fromFile);

    }
}
