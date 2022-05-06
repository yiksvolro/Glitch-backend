using AutoMapper;
using Glitch.ApiModels;
using Glitch.Helpers.Models;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class UserService : BaseService<UserApiModel, User>, IUserService
    {
        private new readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, UserManager<User> userManager, IUserRepository userRepository) : base(userRepository, mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<UserApiModel> GetUserByUserEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ApiException("Can't find user by this email");
            return _mapper.Map<UserApiModel>(user);

        }

        public async Task<UserApiModel> GetUserByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new ApiException("Can't find user by this id");
            return _mapper.Map<UserApiModel>(user);
        }
    }
}
