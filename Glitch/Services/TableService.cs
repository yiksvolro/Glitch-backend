using AutoMapper;
using Glitch.ApiModels;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class TableService : BaseService<TableApiModel, Table>, ITableService
    {
        private readonly IPlaceService _placeService;
        private readonly IBookingService _bookingService;
        public TableService(ITableRepository repository, IMapper mapper, IPlaceService placeService, IBookingService bookingService) : base(repository, mapper)
        {
            _placeService = placeService;
            _bookingService = bookingService;
        }

        public async Task<List<TableApiModel>> GetByPlaceId(int placeId)
        {
            var tempResult = await _repository.GetAllAsync(x => x.PlaceId == placeId);
            if (tempResult.Count == 0) throw new ApiException($"Tables by place {placeId} not found");

            var bookings = await _bookingService.GetForTodayByPlace(placeId);
            if(bookings.Count > 0)
            {
                var tablesToUpdate = tempResult.Where(item => bookings.Select(b => b.TableId).Contains(item.Id)).ToList();
                tablesToUpdate.ForEach(table => table.IsFree = false);
                foreach(var table in tablesToUpdate)
                {
                    await _repository.UpdateAsync(table);
                }
            }
            var result = await _repository.GetAllAsync(x => x.PlaceId == placeId);
            return _mapper.Map<List<TableApiModel>>(result);
        }

        public async Task<TableApiModel> MakeTableFree(int tableId)
        {
            var table = await _repository.GetByAsync(table => table.Id == tableId);
            if (table == null) throw new ApiException($"Table with id = {tableId} not found.");
            
            var place = await _placeService.GetByIdAsync(table.PlaceId);
            place.FreeTables += 1;
            await _placeService.UpdateAsync(place);

            table.IsFree = true;
            return _mapper.Map<TableApiModel>(await _repository.UpdateAsync(table));
        }

        public async Task<TableApiModel> MakeTableNonFree(int tableId)
        {
            var table = await _repository.GetByAsync(table => table.Id == tableId);
            if (table == null) throw new ApiException($"Table with id = {tableId} not found.");

            var place = await _placeService.GetByIdAsync(table.PlaceId);
            place.FreeTables -= 1;
            await _placeService.UpdateAsync(place);

            table.IsFree = false;
            return _mapper.Map<TableApiModel>(await _repository.UpdateAsync(table));
        }
    }
}
