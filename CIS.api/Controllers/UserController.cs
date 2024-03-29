using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _context.Users.Include("Roles").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")] //
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _context.Users.Include("Roles").FirstOrDefaultAsync(s => s.Id == id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    await _context.Users.AddAsync(user);
                    _context.Entry(user.Roles).State = EntityState.Unchanged;
                    await _context.SaveChangesAsync();
                   
                }
                else
                    return BadRequest("User data is incomplete");
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(user);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
                return BadRequest();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
                return NotFound();

            _context.Users.Remove(villa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}