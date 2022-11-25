using CIS.DAL;
using CIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIS.api.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
        public LoginController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var exx = new Exception("User credentials entered are incorrect", ex);
                return BadRequest(exx);
            }


        }
    }
}
