using Glitch.ApiModels;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetPagedFreePlaces([FromQuery]BasePageModel model)
        {
            return Ok(await _placeService.GetPagedFreePlaces(model));
        }
        [HttpGet("{placeId}")]
        public async Task<IActionResult> GetTablesByPlace(int placeId)
        {
            return Ok(await _tableService.GetByPlaceId(placeId));
        }
    }
}
