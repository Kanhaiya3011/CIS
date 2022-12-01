using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ApplicationDbContext _context;
        public CategoryController(ILogger<CategoryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cat = await _context.Categories.ToListAsync();
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
                var cat = await _context.Categories.FindAsync(id);
                return Ok(cat);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
 
    }
}