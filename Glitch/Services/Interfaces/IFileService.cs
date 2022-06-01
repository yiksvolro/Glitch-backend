using Glitch.ApiModels;
using Glitch.Helpers.Enum;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IFileService : IBaseService<FileApiModel>
    {
        Task<FileApiModel> GetFile(FileType type, int placeId);
    }
}
