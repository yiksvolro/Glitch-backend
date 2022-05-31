using Glitch.ApiModels;
using Glitch.Helpers.Enum;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IStorageService
    {
        Task<CreateUpdate<FileApiModel>> UploadFile(IFormFile file, FileType type, int placeId, string description);
    }
}
