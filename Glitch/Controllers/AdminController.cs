using Glitch.ApiModels;
using Glitch.ApiModels.Lists;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public AdminController(IPlaceService placeService, ITableService tableService)
        {
            _placeService = placeService;
            _tableService = tableService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlace(PlaceApiModel model)
        {
            var res = await _placeService.CreateAsync(model);
            if (res.Success) return Ok(res);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTablesAndAssign(ListTableApiModel listTables, [FromQuery]int placeId)
        {
            listTables.Tables.ForEach(table => table.PlaceId = placeId);
            var res = await _tableService.CreateRangeAsync(listTables.Tables);
            if(res.All(table => table.Success)) return Ok(res);
            return NotFound();
        }
    }
}
