using AutoMapper;
using Glitch.ApiModels;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;

namespace Glitch.Services
{
    public class PlaceService : BaseService<PlaceApiModel, Place>, IPlaceService
    {
        public PlaceService(IPlaceRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
