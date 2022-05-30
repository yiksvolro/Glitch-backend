using Glitch.ApiModels;
using Glitch.ApiModels.Lists;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly ITableService _tableService;
        private readonly IUserService _userService;
        public AdminController(IPlaceService placeService, ITableService tableService, IUserService userService)
        {
            _placeService = placeService;
            _tableService = tableService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlace(PlaceApiModel model)
        {
            var res = await _placeService.CreateAsync(model);
            if (res.Success) return Ok(res);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTablesAndAssign(List<TableApiModel> listTables)
        {
            var res = await _tableService.CreateRangeAsync(listTables);
            
            var placeToUpdate = await _placeService.GetByIdAsync(listTables.FirstOrDefault().PlaceId);
            placeToUpdate.AllTables += listTables.Count();
            placeToUpdate.FreeTables += listTables.Count();
            await _placeService.UpdateAsync(placeToUpdate);

            if(res.All(table => table.Success)) return Ok(res);
            return NotFound();
        }
        [HttpDelete("{placeId}")]
        public async Task<IActionResult> DeletePlace(int placeId)
        {
            return Ok(await _placeService.DeleteAsync(placeId));
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userService.GetUserByUserId(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(BasePageModel model)
        {
            var user = await _userService.GetPageUser(model);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllRolesById(int userId)
        {
            var roles = await _userService.GetUserRolesById(userId);
            if (roles != null && roles.Any()) return Ok(roles);

            return NotFound();
        }
        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetUsersByRoleName(string roleName, [FromQuery] BasePageModel model)
        {
            return Ok(await _userService.GetUsersByRoleName(roleName, model));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _userService.GetAllRoles();
            if (roles != null && roles.Any()) return Ok(roles);

            return NotFound();
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserRoles(int userId, [FromBody] List<string> roles)
        {
            if (await _userService.UpdateUserRoles(userId, roles))
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
