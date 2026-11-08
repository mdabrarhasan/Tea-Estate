using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.FileUpload;

namespace TeaForce.Controllers.File
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpPost]
        [Route("FileUpload")]
        public async Task<IActionResult> UploadFile([FromQuery] string userFolder, IFormFile formFile)
        {
            if (string.IsNullOrEmpty(userFolder))
            {
                return BadRequest(new { message = "User folder is required" });
            }

            if (formFile == null)
            {
                return BadRequest(new { message = "File is required" });
            }

            var loginRequest = new AuthRequest
            {
                userid = "Dummy_String",
                password = "Dummy_String",
                devicedetails = "Dummy_String",
                channel = "Mobile"
            };

            var loginResponse = await _fileRepository.authAsync(loginRequest);
            if (loginResponse == null || !loginResponse.status)
            {
                return Unauthorized(new { message = "Login failed" });
            }

            var uploadResponse = await _fileRepository.UploadFileAsync(formFile, loginResponse.token, userFolder);
            if (uploadResponse == null || !uploadResponse.status)
            {
                return BadRequest(new { message = "File upload failed" });
            }

            return Ok(uploadResponse);
        }

        [HttpPost]
        [Route("FileDownload")]
        public async Task<IActionResult> DownloadFileAsBase64([FromQuery] string userFolder)
        {
            if (string.IsNullOrEmpty(userFolder))
            {
                return BadRequest(new { message = "User folder is required" });
            }

            var loginRequest = new AuthRequest
            {
                userid = "Dummy_String",
                password = "Dummy_String",
                devicedetails = "Dummy_String",
                channel = "Mobile"
            };

            var loginResponse = await _fileRepository.authAsync(loginRequest);

            if (loginResponse == null || !loginResponse.status)
            {
                return Unauthorized(new { message = "Login failed" });
            }

            var downloadResponse = await _fileRepository.DownloadFileAsBase64Async(userFolder, loginResponse.token);
            if (downloadResponse == null || !downloadResponse.status)
            {
                return BadRequest(new { message = "File download failed" });
            }

            return Ok(downloadResponse);
        }
    }
}
