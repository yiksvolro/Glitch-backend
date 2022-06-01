using AutoMapper;
using Glitch.ApiModels;
using Glitch.Helpers.Enum;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class FileService : BaseService<FileApiModel, File>, IFileService
    {
        public FileService(IFileRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<FileApiModel> GetFile(FileType type, int placeId)
        {
            var result = await _repository.GetByAsync(x => x.Type == type && x.PlaceId == placeId);
            if (result == null) throw new ApiException($"There is no {type} file for Place {placeId}");
            return _mapper.Map<FileApiModel>(result);
        }
    }
}
