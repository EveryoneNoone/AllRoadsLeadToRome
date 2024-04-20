using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserSessionsController: ControllerBase
    {
        private readonly IRepository<UserSession> _userSessionRepository;

        public UserSessionsController(IRepository<UserSession> userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        // GET: api/UserSessions
        [HttpGet]
        public async Task<IActionResult> GetUserSessions(CancellationToken token)
        {
            var userSessions = await _userSessionRepository.GetAllAsync(token);
            return Ok(userSessions);
        }

        // GET: api/UserSessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSession(Guid id, CancellationToken token)
        {
            var userSession = await _userSessionRepository.GetByIdAsync(id, token);
            if (userSession == null)
            {
                return NotFound();
            }
            return Ok(userSession);
        }

        // POST: api/UserSessions
        [HttpPost]
        public async Task<IActionResult> PostUserSession([FromBody] UserSession userSession, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userSessionRepository.AddAsync(userSession, token);
            return CreatedAtAction("GetUserSession", new { id = userSession.Id }, userSession);
        }

        // PUT: api/UserSessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSession(Guid id, [FromBody] UserSession userSession, CancellationToken token)
        {
            if (id != userSession.Id)
            {
                return BadRequest();
            }

            await _userSessionRepository.UpdateAsync(userSession, token);
            return NoContent();
        }

        // DELETE: api/UserSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSession(Guid id, CancellationToken token)
        {
            var userSession = await _userSessionRepository.GetByIdAsync(id, token);
            if (userSession == null)
            {
                return NotFound();
            }

            await _userSessionRepository.DeleteAsync(id, token);
            return NoContent();
        }   
    }
}
