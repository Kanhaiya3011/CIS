using Microsoft.AspNetCore.Mvc;

namespace CIS.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
