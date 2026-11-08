namespace TEALINK.Models.FileUpload
{
    public class FileUploadResponse
    {
        public string code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string responsetime { get; set; }
        public string fileResponse { get; set; }
        public string base64FileResponse { get; set; }
    }

    public class MultiFileUploadResponse
    {
        public string code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string responsetime { get; set; }
        public string fileResponse { get; set; }
        public List<Base64FileResponse> base64FileResponse { get; set; }
    }

    public class Base64FileResponse
    {
        public string base64Content { get; set; }
        public string fileName { get; set; }
    }

    public class DownloadResponse
    {
        public string code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string responsetime { get; set; }
        public List<Base64FileResponse> base64FileResponse { get; set; }
    }

    public class MultiFileDownloadResponse
    {
        public string code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string responsetime { get; set; }
        public string fileResponse { get; set; }
        public List<Base64FileResponse> base64FileResponse { get; set; }
    }
}
