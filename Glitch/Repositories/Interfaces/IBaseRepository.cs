using Glitch.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Repositories.Interfaces
{
    public interface IBaseRepository<TModel> where TModel : class, IBaseModel
    {
        Task<TModel> CreateAsync(TModel model);
        Task<IEnumerable<TModel>> CreateRangeAsync(ICollection<TModel> models);

        Task<TModel> UpdateAsync(TModel model);

        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteRangeAsync(IEnumerable<TModel> entities);

        Task<TModel> GetByAsync(Func<TModel, bool> filter);

        Task<List<TModel>> GetAllAsync();

        Task<List<TModel>> GetAllAsync(Func<TModel, bool> filter);
    }
}
