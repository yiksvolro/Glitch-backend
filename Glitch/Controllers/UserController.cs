using Glitch.ApiModels;
using Glitch.ApiModels.UserModels;
using Glitch.Extensions;
using Glitch.Models;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Glitch.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var time = DateTime.UtcNow.GetUkrainianDateTime();
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    EmailConfirmed = true,
                    CreatedAt = time,
                    UpdatedAt = time
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var newUser = await _userManager.FindByEmailAsync(user.Email);

                    await _userManager.AddToRolesAsync(newUser, new[] { "User" });
                    var data = await _userService.GetUserByUserEmail(model.Email);
                    return Ok(data);
                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {

                    var user = await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(user, false);
                    var data = await _userService.GetUserByUserEmail(model.Email);

                    if (data != null)
                    {
                        return Ok(data);
                    }
                }

            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(true);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeSelfPassword([FromBody] ChangePassword model)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user.Id == int.Parse(userId))
            {
                IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (passwordChangeResult.Succeeded)
                {
                    return Ok(passwordChangeResult.Succeeded);
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateSelf([FromBody] UpdateUser model)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
            var user = await _userManager.FindByIdAsync(userId);
            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName; 
            user.UpdatedAt = DateTime.UtcNow.GetUkrainianDateTime();
            await _userManager.UpdateAsync(user);

            var res = await _userService.GetUserByUserEmail(model.Email);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrent()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserByUserId(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyRoles()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.GetUserRolesById(userId);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _userService.DeleteAsync(Convert.ToInt32(userId));
            if (res)
            {
                return Ok("Your account was deleted successfully.");
            }
            return NotFound();
        }
    }
}
