using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_risk_hero.Contracts;

namespace student_risk_hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BlobStoragesController : ControllerBase
    {
        private readonly IBlobStorageService _storage;

        public BlobStoragesController(IBlobStorageService storage)
        {
            _storage = storage;
        }

        [HttpGet("ListFiles")]
        public IActionResult ListFiles()
        {
            return Ok(_storage.GetAllDocuments());
        }

        [Route("InsertFile")]
        [HttpPost]
        public IActionResult InsertFile([FromForm] IFormFile asset)
        {
            if (asset != null)
            {
                Stream stream = asset.OpenReadStream();
                _storage.UploadDocument(asset.FileName, stream);
                return Ok(true);
            }

            return Ok(false);
        }

        [HttpGet("DownloadFile/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var content = _storage.GetDocument(fileName);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Route("DeleteFile/{fileName}")]
        [HttpGet]
        public IActionResult DeleteFile(string fileName)
        {
            return Ok(_storage.DeleteDocument(fileName));
        }
    }
}
