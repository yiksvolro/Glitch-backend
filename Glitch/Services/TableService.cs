using AutoMapper;
using Glitch.ApiModels;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;

namespace Glitch.Services
{
    public class TableService : BaseService<TableApiModel, Table>, ITableService
    {
        public TableService(ITableRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
