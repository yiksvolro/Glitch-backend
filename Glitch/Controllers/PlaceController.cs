using Glitch.ApiModels;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly ITableService _tableService;
        public PlaceController(IPlaceService placeService, ITableService tableService)
        {
            _placeService = placeService;
            _tableService = tableService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPagedPlaces([FromQuery]BasePageModel model, bool isOnlyFree, string filter = null)
        {
            var result = await _placeService.GetPagedPlaces(model, isOnlyFree, filter);
            return Ok(result);
        }
        [HttpGet("{placeId}")]
        public async Task<IActionResult> GetTablesByPlace(int placeId)
        {
            return Ok(await _tableService.GetByPlaceId(placeId));
        }
        [HttpGet("{placeId}")]
        public async Task<IActionResult> GetPlaceById(int placeId)
        {
            return Ok(await _placeService.GetByIdAsync(placeId));
        }
    }
}
