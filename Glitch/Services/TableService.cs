using AutoMapper;
using Glitch.ApiModels;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class TableService : BaseService<TableApiModel, Table>, ITableService
    {
        private readonly IPlaceService _placeService;
        public TableService(ITableRepository repository, IMapper mapper, IPlaceService placeService) : base(repository, mapper)
        {
            _placeService = placeService;
        }

        public async Task<List<TableApiModel>> GetByPlaceId(int placeId)
        {
            var result = await _repository.GetAllAsync(x => x.PlaceId == placeId);
            if (result.Count == 0) throw new ApiException($"Tables by place {placeId} not found");
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
