using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IUserService: IBaseService<UserApiModel>
    {
        Task<UserApiModel> GetUserByUserEmail(string email);
        Task<UserApiModel> GetUserByUserId(string id);
        Task<PagedResult<UserApiModel>> GetPageUser(BasePageModel model);
    }
}
