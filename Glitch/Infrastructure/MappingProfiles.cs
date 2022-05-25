using AutoMapper;
using Glitch.ApiModels;
using Glitch.Models;

namespace Glitch.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserApiModel>();
            CreateMap<UserApiModel, User>();

            CreateMap<Place, PlaceApiModel>();
            CreateMap<PlaceApiModel, Place>();

            CreateMap<Table, TableApiModel>();
            CreateMap<TableApiModel, Table>();

            CreateMap<Booking, BookingApiModel>();
            CreateMap<BookingApiModel, Booking>();
        }
    }
}
