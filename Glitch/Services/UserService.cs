using AutoMapper;
using Glitch.ApiModels;
using Glitch.ApiModels.PaginationModels;
using Glitch.Helpers.Models;
using Glitch.Helpers.Seed;
using Glitch.Models;
using Glitch.Repositories.Interfaces;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Services
{
    public class UserService : BaseService<UserApiModel, User>, IUserService
    {
        private new readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<AppRole> _roleManager;
        public UserService(IMapper mapper, UserManager<User> userManager, IUserRepository userRepository, RoleManager<AppRole> roleManager) : base(userRepository, mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        public async Task<PagedResult<UserApiModel>> GetPageUser(BasePageModel model)
        {
            return await _userRepository.GetPageAsync<UserApiModel>(model.Page, model.PageSize);
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
        public async Task<List<string>> GetUserRolesById(int userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<List<string>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(x => x.Name).ToList();
        }
        public async Task<bool> UpdateUserRoles(int userId, IEnumerable<string> roles)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                var userRoles = await _userManager.GetRolesAsync(user);

                var rolesToRemove = userRoles.Where(p => roles.All(p2 => p2 != p));
                var rolesToAdd = roles.Where(p => userRoles.All(p2 => p2 != p));

                await _userManager.AddToRolesAsync(user, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<UserApiModel>> GetUsersByRoleName(string roleName)
        {
            var result = await _userManager.GetUsersInRoleAsync(roleName);
            if (result == null) throw new ApiException($"There is no users with role {roleName}");
            return _mapper.Map<List<UserApiModel>>(result);
        }
    }
}
