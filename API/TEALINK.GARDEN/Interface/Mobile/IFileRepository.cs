
using TEALINK.Models.FileUpload;

namespace TEALINK.GARDEN.Interface.Mobile
{
    public interface IFileRepository
    {
        Task<AuthResponse> authAsync(AuthRequest loginRequest);
        Task<FileUploadResponse> UploadFileAsync(IFormFile formFile, string token, string userFolder);
        Task<FileUploadResponse> MultiFilesUploadAsync(IEnumerable<IFormFile> formFiles, string token, string userFolder);
        Task<DownloadResponse> DownloadFileAsBase64Async(string userFolder, string token);
        Task<MultiFileDownloadResponse> MultiFilesDownloadFileAsBase64Async(string userFolder, string token);
    }
}
