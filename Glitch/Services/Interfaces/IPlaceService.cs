using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IPlaceService : IBaseService<PlaceApiModel>
    {
        public Task<PagedResult<PlaceApiModel>> GetPagedFreePlaces(BasePageModel model);
    }
}
