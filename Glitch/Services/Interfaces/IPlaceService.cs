using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IPlaceService : IBaseService<PlaceApiModel>
    {
        Task<PagedResult<PlaceApiModel>> GetPagedPlaces(BasePageModel model, bool isOnlyFree, string filter = null);
        Task<List<PlaceApiModel>> GetPlacesByOwner(int userId);
    }
}
