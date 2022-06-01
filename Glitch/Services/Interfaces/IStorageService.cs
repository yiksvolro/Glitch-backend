using Glitch.ApiModels;
using Glitch.ApiModels.ResponseModel;
using Glitch.Helpers.Enum;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IStorageService
    {
        Task<CreateUpdate<FileApiModel>> UploadFile(IFormFile file, FileType type, int placeId, string description);
        Task<FileResponseModel> DownloadFile(FileType type, int placeId);
        Task<bool> DeleteFile(FileType type, int placeId);
    }
}
