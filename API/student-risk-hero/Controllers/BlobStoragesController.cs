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
        private readonly string _connectionString;
        private readonly string _container;

        public BlobStoragesController(IBlobStorageService storage, IConfiguration iConfig)
        {
            _storage = storage;
            _connectionString = iConfig.GetValue<string>("StorageSettings:StorageConnection");
            _container = iConfig.GetValue<string>("StorageSettings:ContainerName");
        }

        [HttpGet("ListFiles")]
        public async Task<List<string>> ListFiles()
        {
            return await _storage.GetAllDocuments(_connectionString, _container);
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<bool> InsertFile([FromForm] IFormFile asset)
        {
            if (asset != null)
            {
                Stream stream = asset.OpenReadStream();
                await _storage.UploadDocument(_connectionString, _container, asset.FileName, stream);
                return true;
            }

            return false;
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var content = await _storage.GetDocument(_connectionString, _container, fileName);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Route("DeleteFile/{fileName}")]
        [HttpGet]
        public async Task<bool> DeleteFile(string fileName)
        {
            return await _storage.DeleteDocument(_connectionString, _container, fileName);
        }
    }
}
