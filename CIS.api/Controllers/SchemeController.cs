using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        //[HttpGet("/GetNotAppliedSchemes/{val:int}")]
        //public async Task<IActionResult> GetNotAppliedSchemes(int val)
        //{
        //    var schemes = await _context.Schemes.ToListAsync();
        //    var appliedSchemes = await _context.BeneficiarySchemeApplied.Where(s => s.Beneficiary == val).ToListAsync();
        //    var appliedSchems = schemes.Join(appliedSchemes,
        //            s => s.Id,
        //            a => a.Scheme,
        //            (ls, la) => new { ls, la }).Select(c => c.ls).ToList();

        //    var notAppliedSchemes = schemes.Except(appliedSchems).ToList();
        //    return Ok(notAppliedSchemes);
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var schemes = await _context.Schemes.Include("Category").ToListAsync();
                return Ok(schemes);
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
                var scheme = await _context.Schemes.Include("Category").FirstOrDefaultAsync(s => s.Id == id);
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

                    _context.Entry(scheme.Category).State = EntityState.Unchanged;
                    _context.SaveChanges();

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
            _context.Entry(scheme.Category).State = EntityState.Unchanged;
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