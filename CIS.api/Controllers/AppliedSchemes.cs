using CIS.DAL;
using CIS.Models;
using CIS.Models.Relations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class AppliedSchemesController : BaseController
    {
        private readonly ILogger<AppliedSchemesController> _logger;
        private readonly ApplicationDbContext _context;
        public AppliedSchemesController(ILogger<AppliedSchemesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bens = await _context.BeneficiarySchemeApplied.ToListAsync();
                return Ok(bens);
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
                var ben = await _context.BeneficiarySchemeApplied.FindAsync(id);
                return Ok(ben);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BeneficiarySchemeApplied beneficiary)
        {
            try
            {
                if (beneficiary != null)
                {
                    await _context.BeneficiarySchemeApplied.AddAsync(beneficiary);
                    await _context.SaveChangesAsync();

                }
                else
                    return BadRequest("User data is incomplete");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(beneficiary);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BeneficiarySchemeApplied beneficiary)
        {
            if (beneficiary == null || id != beneficiary.Id)
                return BadRequest();

            _context.BeneficiarySchemeApplied.Update(beneficiary);
            await _context.SaveChangesAsync();

            return Ok(beneficiary);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var ben = await _context.BeneficiarySchemeApplied.FirstOrDefaultAsync(u => u.Id == id);
            if (ben == null)
                return NotFound();

            _context.BeneficiarySchemeApplied.Remove(ben);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}