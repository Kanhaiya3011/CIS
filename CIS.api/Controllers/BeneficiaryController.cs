using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class SchemeController : BaseController
    {
        private readonly ILogger<SchemeController> _logger;
        private readonly ApplicationDbContext _context;
        public SchemeController(ILogger<SchemeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _context.Schemes.ToListAsync();
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
                var scheme = await _context.Schemes.FindAsync(id);
                return Ok(scheme);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Scheme scheme)
        {
            try
            {
                if (scheme != null)
                {
                    await _context.Schemes.AddAsync(scheme);
                    await _context.SaveChangesAsync();
                   
                }
                else
                    return BadRequest("User data is incomplete");
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(scheme);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Scheme scheme)
        {
            if (scheme == null || id != scheme.Id)
                return BadRequest();

            _context.Schemes.Update(scheme);
            await _context.SaveChangesAsync();

            return Ok(scheme);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var scheme = await _context.Schemes.FirstOrDefaultAsync(u => u.Id == id);
            if (scheme == null)
                return NotFound();

            scheme.IsActive = false;
            _context.Schemes.Update(scheme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}