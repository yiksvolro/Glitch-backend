using Glitch.ApiModels;
using Glitch.Extensions;
using Glitch.Helpers.Models;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Glitch.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITableService _tableService;
        public BookingController(IBookingService bookingService, ITableService tableService)
        {
            _bookingService = bookingService;
            _tableService = tableService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingApiModel model)
        {
            model.UserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var today = DateTime.UtcNow.GetUkrainianDateTime();
            model.Number = _tableService.GetByIdAsync(model.TableId).Result.Number;
            if (model.BookedOn < today) throw new ApiException("Date and time for booking is wrong!");
            return Ok(await _bookingService.CreateAsync(model));
        }
        [HttpGet]
        public async Task<IActionResult> GetMyBookings([FromQuery] BasePageModel model)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _bookingService.GetBookingsByUserId(userId, model);
            return Ok(result);
        }
        [HttpGet]
        [Authorize(Roles ="PlaceOwner")]
        public async Task<IActionResult> GetBookingsForTodayByPlace(int placeId)
        {
            var result = await _bookingService.GetForTodayByPlace(placeId);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingApiModel model)
        {
            model.Number = _tableService.GetByIdAsync(model.TableId).Result.Number;
            return Ok(await _bookingService.UpdateAsync(model));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var booking = await _bookingService.GetByIdAsync(bookingId);
            if (booking == null) throw new ApiException($"There is no booking with ID {booking}");
            booking.IsArchive = true;
            return Ok(await _bookingService.UpdateAsync(booking));
        }

    }
}
