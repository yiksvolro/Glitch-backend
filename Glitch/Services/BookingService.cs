using AutoMapper;
using Glitch.ApiModels;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;

namespace Glitch.Services
{
    public class BookingService : BaseService<BookingApiModel, Booking>, IBookingService
    {
        public BookingService(IBookingRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
