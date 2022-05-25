using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Authorize(Roles = "PlaceOwner")]
    [Route("api/[controller]/[action]")]
    public class PlaceOwnerController: Controller
    {
        private readonly ITableService _tableService;
        private readonly IPlaceService _placeService;
        public PlaceOwnerController(ITableService tableService, IPlaceService placeService)
        {
            _tableService = tableService;
            _placeService = placeService;
        }

        [HttpPut]
        public async Task<IActionResult> FreeTable(int tableId)
        {
            return Ok(await _tableService.MakeTableFree(tableId));
        }
        [HttpPut]
        public async Task<IActionResult> OccupyTable(int tableId)
        {
            return Ok(await _tableService.MakeTableNonFree(tableId));
        }
    }
}
