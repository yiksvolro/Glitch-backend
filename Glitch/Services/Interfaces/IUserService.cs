using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IUserService: IBaseService<UserApiModel>
    {
        Task<UserApiModel> GetUserByUserEmail(string email);
        Task<UserApiModel> GetUserByUserId(string id);
        Task<PagedResult<UserApiModel>> GetPageUser(BasePageModel model);
        Task<List<string>> GetUserRolesById(int userId);
        Task<List<string>> GetAllRoles();
        Task<bool> UpdateUserRoles(int userId, IEnumerable<string> roles);
        Task<List<UserApiModel>> GetUsersByRoleName(string roleName);
    }
}
