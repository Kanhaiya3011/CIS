using CIS.Models;
using CIS.Web.Helper;
using CIS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiUrl = "http://localhost:7042/api/";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            var url = $"{_apiUrl}User";
            var content = Utilities.SerializeObject(user);
            var response = await Utilities.HttpGetCall(url);
            var result = await response.Content.ReadAsStringAsync();
            if(result != null)
                return View("", Utilities.DeSerializeObject(result, User));
            else
            {
                ViewBag.Message = "User credentials are incorrect";
                return View("Index");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}