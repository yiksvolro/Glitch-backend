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
        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
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
            return Ok(await _storageService.DownloadFile(type, placeId));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin,PlaceOwner")]
        public async Task<IActionResult> DeleteFile(FileType type, int placeId)
        {
            return Ok(await _storageService.DeleteFile(type, placeId));
        }
    }
}
