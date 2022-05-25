using AutoMapper;
using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class PlaceService : BaseService<PlaceApiModel, Place>, IPlaceService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPlaceRepository _placeRepository;
        public PlaceService(IPlaceRepository repository, IMapper mapper, IBookingRepository bookingRepository, IPlaceRepository placeRepository) : base(repository, mapper)
        {
            _bookingRepository = bookingRepository;
            _placeRepository = placeRepository;
        }

        public async Task<PagedResult<PlaceApiModel>> GetPagedFreePlaces(BasePageModel model)
        {
            var result = await _placeRepository.GetPageAsync<PlaceApiModel>(x => x.FreeTables > 0, model.Page, model.PageSize);
            if (result == null) throw new ApiException("There is no free places");
            return result;
        }
    }
}
