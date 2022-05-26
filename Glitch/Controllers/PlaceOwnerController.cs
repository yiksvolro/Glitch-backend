using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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
        [HttpGet]
        public async Task<IActionResult> GetMyPlaces()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _placeService.GetPlacesByOwner(userId));
        }
    }
}
