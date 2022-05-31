using AutoMapper;
using Glitch.ApiModels;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;

namespace Glitch.Services
{
    public class FileService : BaseService<FileApiModel, File>, IFileService
    {
        public FileService(IFileRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
