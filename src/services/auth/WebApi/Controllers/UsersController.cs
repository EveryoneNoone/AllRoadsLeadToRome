namespace WebApi.Controllers;

using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var users = await _userRepository.GetAllAsync(token);
        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(id, token);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST: api/Users
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User user, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _userRepository.AddAsync(user, token);
        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, [FromBody] User user, CancellationToken token)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        await _userRepository.UpdateAsync(user, token);

        return NoContent();
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetByIdAsync(id, token);
        if (user == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteAsync(id, token);

        return NoContent();
    }
}

