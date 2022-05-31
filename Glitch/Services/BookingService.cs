using AutoMapper;
using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using Glitch.Extensions;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class BookingService : BaseService<BookingApiModel, Booking>, IBookingService
    {
        public BookingService(IBookingRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<PagedResult<BookingApiModel>> GetBookingsByUserId(int userId, BasePageModel model)
        {
            return await _repository.GetPageAsync<BookingApiModel>(
                 x => x.UserId == userId,
                 model.Page,
                 model.PageSize,
                  n => n.BookedOn);


        }

        public async Task<List<BookingApiModel>> GetForTodayByPlace(int placeId)
        {
            var today = DateTime.UtcNow.GetUkrainianDateTime();
            var result = await _repository.GetAllAsync(x => x.PlaceId == placeId && x.BookedOn.Date == today.Date && x.IsArchive == false);
            //if (result.Count == 0) throw new ApiException("There is no bookings for today");
            return _mapper.Map<List<BookingApiModel>>(result);
        }
    }
}
