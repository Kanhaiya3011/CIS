using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class RoleController : BaseController
    {
        private readonly ILogger<RoleController> _logger;
        private readonly ApplicationDbContext _context;
        public RoleController(ILogger<RoleController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cat = await _context.Roles.ToListAsync();
                return Ok(cat);
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
                var cat = await _context.Roles.FindAsync(id);
                return Ok(cat);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
 
    }
}