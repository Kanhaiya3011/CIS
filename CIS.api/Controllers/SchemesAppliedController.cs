using CIS.DAL;
using CIS.Models.Relations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class SchemesAppliedController : BaseController
    {
        private readonly ILogger<SchemesAppliedController> _logger;
        private readonly ApplicationDbContext _context;
        public SchemesAppliedController(ILogger<SchemesAppliedController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(BeneficiarySchemeApplied applied)
        {
            try
            {
                if (applied == null)
                    return BadRequest("Values not correct");

                _context.BeneficiarySchemeApplied.Add(applied);
                await _context.SaveChangesAsync();

                return Ok(applied);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appliedSchems = await _context.BeneficiarySchemeApplied.ToListAsync();
            return Ok(appliedSchems);
        }

        [HttpGet("{id}")] //
        public async Task<IActionResult> Get(int id)
        {
            var schemes = await _context.Schemes.ToListAsync();
            var appliedSchemes = await _context.BeneficiarySchemeApplied.Where(s => s.Beneficiary == id).ToListAsync();
            var appliedSchems = schemes.Join(appliedSchemes,
                    s => s.Id,
                    a => a.Scheme,
                    (ls, la) => new { ls, la }).Select(c => c.ls).ToList();

            var notAppliedSchemes = schemes.Except(appliedSchems).ToList();
            return Ok(notAppliedSchemes);
        }
    }
}
