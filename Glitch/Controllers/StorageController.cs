using Glitch.Helpers.Enum;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;
        private readonly IFileService _fileService;
        public StorageController(IStorageService storageService, IFileService fileService)
        {
            _storageService = storageService;
            _fileService = fileService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PlaceOwner")]
        public async Task<IActionResult> UploadFile(IFormFile file, FileType type, int placeId, string description)
        {
            return Ok(
                await _storageService.UploadFile(file, type, placeId, description));
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFile(FileType type, int placeId)
        {
            var result = await _storageService.DownloadFile(type, placeId);
            return File(result.File, result.ContentType);
        }
        [HttpGet]
        public async Task<IActionResult> GetDescription(FileType type, int placeId)
        {
            return Ok((await _fileService.GetFile(type, placeId)).Description);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            return Ok(await _fileService.GetAllAsync());
        }
        [HttpDelete]
        [Authorize(Roles = "Admin,PlaceOwner")]
        public async Task<IActionResult> DeleteFile(FileType type, int placeId)
        {
            return Ok(await _storageService.DeleteFile(type, placeId));
        }
    }
}
