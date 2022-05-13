using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glitch.Repositories
{
    public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(GlitchContext glitchContext, ILogger<BaseRepository<Place>> logger) : base(glitchContext, logger)
        {
        }
    }
}
