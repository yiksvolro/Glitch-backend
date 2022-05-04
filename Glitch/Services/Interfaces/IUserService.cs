using Glitch.ApiModels;
using System.Threading.Tasks;

namespace Glitch.Services.Interfaces
{
    public interface IUserService: IBaseService<UserApiModel>
    {
        Task<UserApiModel> GetUserByUserEmail(string email);
    }
}
