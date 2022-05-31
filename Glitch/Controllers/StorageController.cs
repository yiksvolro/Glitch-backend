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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadFile(IFormFile file, FileType type, int placeId, string description)
        {
            return Ok(
                await _storageService.UploadFile(file, type, placeId, description));
        }
    }
}
