using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class BeneficiaryController : BaseController
    {
        private readonly ILogger<BeneficiaryController> _logger;
        private readonly ApplicationDbContext _context;
        public BeneficiaryController(ILogger<BeneficiaryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _context.Beneficiaries.ToListAsync();
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
                var user = await _context.Beneficiaries.FindAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Beneficiary beneficiary)
        {
            try
            {
                if (beneficiary != null)
                {
                    await _context.Beneficiaries.AddAsync(beneficiary);
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
        public async Task<IActionResult> Put(int id, [FromBody] Beneficiary beneficiary)
        {
            if (beneficiary == null || id != beneficiary.Id)
                return BadRequest();

            _context.Beneficiaries.Update(beneficiary);
            await _context.SaveChangesAsync();

            return Ok(beneficiary);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var ben = await _context.Beneficiaries.FirstOrDefaultAsync(u => u.Id == id);
            if (ben == null)
                return NotFound();

            _context.Beneficiaries.Remove(ben);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}