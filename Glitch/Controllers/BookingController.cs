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
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingApiModel model)
        {
            model.UserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var today = DateTime.UtcNow.GetUkrainianDateTime();
            if (model.BookedOn < today) throw new ApiException("Date and time for booking is wrong!");
            return Ok(await _bookingService.CreateAsync(model));
        }
        [HttpGet]
        public async Task<IActionResult> GetMyBookings([FromQuery] BasePageModel model)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await _bookingService.GetBookingsByUserId(userId, model));
        }
        [HttpGet]
        [Authorize(Roles ="PlaceOwner")]
        public async Task<IActionResult> GetBookingsForTodayByPlace(int placeId)
        {
            return Ok(await _bookingService.GetForTodayByPlace(placeId));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingApiModel model)
        {
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
