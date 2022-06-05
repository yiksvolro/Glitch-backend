using AutoMapper;
using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class PlaceService : BaseService<PlaceApiModel, Place>, IPlaceService
    {
        private readonly IBookingRepository _bookingRepository;
        public PlaceService(IPlaceRepository repository, IMapper mapper, IBookingRepository bookingRepository) : base(repository, mapper)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<PagedResult<PlaceApiModel>> GetPagedPlaces(BasePageModel model, bool isOnlyFree, string filter = null)
        {
            var result = new PagedResult<PlaceApiModel>();
            filter = filter != null ? filter.ToLower() : null;
            if (isOnlyFree && filter != null)
            {
                result = await _repository.GetPageAsync<PlaceApiModel>(x => x.FreeTables > 0 && x.ShortName.ToLower().StartsWith(filter), model.Page, model.PageSize);
            }
            else if(filter != null)
            {
                result = await _repository.GetPageAsync<PlaceApiModel>(x => x.ShortName.ToLower().StartsWith(filter), model.Page, model.PageSize);
            }
            else if (isOnlyFree)
            {
                result = await _repository.GetPageAsync<PlaceApiModel>(x => x.FreeTables > 0, model.Page, model.PageSize);
            }
            else
            {
                result = await _repository.GetPageAsync<PlaceApiModel>(model.Page, model.PageSize);
            }
            if (result == null) throw new ApiException("There is no free places");
            return result;
        }

        public async Task<List<PlaceApiModel>> GetPlacesByOwner(int userId)
        {
            var result = await _repository.GetAllAsync(x => x.UserId == userId);
            if (result == null) throw new ApiException("You have no places");
            return _mapper.Map<List<PlaceApiModel>>(result);
        }
    }
}
