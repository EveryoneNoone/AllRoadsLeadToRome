using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Driver,User")]
        public async Task<IActionResult> GetUser(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || (currentUser.Id != id && !User.IsInRole("Administrator")))
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Driver,User")]
        public async Task<IActionResult> PutUser(string id, [FromBody] UpdateUserModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || (currentUser.Id != id && !User.IsInRole("Administrator")))
            {
                return Forbid();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = model.FullName;
            user.DriverApproved = model.DriverApproved;
            user.NotificationPreference = model.NotificationPreference;
            user.Type = model.Type;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
