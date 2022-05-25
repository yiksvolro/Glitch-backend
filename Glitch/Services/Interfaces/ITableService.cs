using Glitch.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface ITableService : IBaseService<TableApiModel> 
    {
        Task<TableApiModel> MakeTableFree(int tableId);
        Task<TableApiModel> MakeTableNonFree(int tableId);
        Task<List<TableApiModel>> GetByPlaceId(int placeId);
    }
}
