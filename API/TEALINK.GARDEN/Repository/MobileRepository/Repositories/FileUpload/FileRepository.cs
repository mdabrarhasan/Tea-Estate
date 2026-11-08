using System.Net.Http.Headers;

using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.FileUpload;

namespace TEALINK.Repositories.FileUpload
{
    public class FileRepository:IFileRepository
    {
        private readonly HttpClient _httpClient;

        public FileRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponse> authAsync(AuthRequest loginRequest)
        {
            var loginUrl = "Dummy_String";
            var response = await _httpClient.PostAsJsonAsync(loginUrl, loginRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<FileUploadResponse> UploadFileAsync(IFormFile formFile, string token, string userFolder)
        {
            var uploadUrl = $"Dummy_String";
            using var content = new MultipartFormDataContent();
            using var fileStream = formFile.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
            content.Add(fileContent, "formFile", formFile.FileName);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync(uploadUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<FileUploadResponse>();
        }


        public async Task<FileUploadResponse> MultiFilesUploadAsync(IEnumerable<IFormFile> formFiles, string token, string userFolder)
        {

            

            var uploadUrl = $"Dummy_String";

            using var content = new MultipartFormDataContent();


            foreach (var formFile in formFiles)
            {
                var fileStream = formFile.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
                content.Add(fileContent, "formFile", formFile.FileName);
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(uploadUrl, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<FileUploadResponse>();
        }



        public async Task<DownloadResponse> DownloadFileAsBase64Async(string userFolder, string token)
        {
            var downloadUrl = $"Dummy_String";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(downloadUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DownloadResponse>();
        }

        public async Task<MultiFileDownloadResponse> MultiFilesDownloadFileAsBase64Async(string userFolder, string token)
        {

            var baseUrl = "Dummy_String";
            var requestUrl = $"Dummy_String";

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MultiFileDownloadResponse>();

        }
    }
}
