using CIS.Models;
using CIS.Web.Helper;
using CIS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CIS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiUrl = @"https://localhost:7042/api";
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
        public async Task<IActionResult> Register(User user)
        {
            var url = $"{_apiUrl}/User";
            user.RoleId = 2;
            user.Status = "Pending";
            var content = Utilities.SerializeObject(user);
            var response = await Utilities.HttpPostCall(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (result != null)
            {
                ViewBag.Message = "User has been created, kindly wait for the Admin to approve the account.";
                return View("Register", Utilities.DeSerializeObject<User>(result));
            }
                
            else
            {
                
                return View("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            var url = $"{_apiUrl}/Login";
            var content = Utilities.SerializeObject(user);
            var response = await Utilities.HttpPostCall(url, content);
            var result = await response.Content.ReadAsStringAsync();
            var userResult = Utilities.DeSerializeObject<User>(result);
            if(userResult != null && userResult.Status != "Approved")
            {
                TempData["AuthFailed"] = "User has not yet been approved by Admin. Contact Admin.";
                return RedirectToAction("Index", "Home");
            }
            else if (userResult != null && userResult.RoleId == 1)
            {
                return RedirectToAction("Index", "Admin", new { area = "" });
            }
            else if (userResult != null && userResult.RoleId == 2)
            {
                return RedirectToAction("Index", "User", new { id = userResult.Id });
            }
            else
            {
                TempData["AuthFailed"] = "User credentials are incorrect. Please try again.";
                return RedirectToAction("Index", "Home");
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