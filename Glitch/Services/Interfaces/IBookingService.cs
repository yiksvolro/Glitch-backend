using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IBookingService : IBaseService<BookingApiModel>
    {
        Task<PagedResult<BookingApiModel>> GetBookingsByUserId(int userId, BasePageModel model);
        Task<List<BookingApiModel>> GetForTodayByPlace(int placeId);
    }
}
