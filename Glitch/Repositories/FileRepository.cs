using AutoMapper;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glitch.Repositories
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(GlitchContext glitchContext, ILogger<BaseRepository<File>> logger, IMapper mapper) : base(glitchContext, logger, mapper)
        {
        }
    }
}
