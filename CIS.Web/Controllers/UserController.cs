using CIS.Models;
using CIS.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace CIS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly string _apiUrl = @"https://localhost:7042/api";
       // [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            //var id = Request.Query["id"];
            if (id != 0)
            {
                var url = $"{_apiUrl}/User/{id}";
                var response = await Utilities.HttpGetCall(url);
                var result = await response.Content.ReadAsStringAsync();
                var user = Utilities.DeSerializeObject<User>(result);
                HttpContext.Session.SetString("LoggedInUser", $"{user?.FirstName} {user?.LastName}");
                TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
                return View("Index", user);
            }
            else
                return BadRequest();
           
        }
        public IActionResult AddBeneficiary()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            var ben = new Beneficiary();
            int? id = 0;
            if (ModelState.IsValid)
            {
                var url = $"{_apiUrl}/User";
                beneficiary.Status = "Pending";
                beneficiary.IsActive = false;
                var content = Utilities.SerializeObject(beneficiary);
                var response = await Utilities.HttpPostCall(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var res = Utilities.DeSerializeObject<Beneficiary>(result);
                id = res?.Id;
            }
            TempData["benCreated"] = $"Successfully created User with the ID : {id}";
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("AddBeneficiary", ben);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ViewBeneficiary(int id)
        {
            if (id == 0)
                return BadRequest();
            var url = $"{_apiUrl}/User/{id}";
            var response = await Utilities.HttpGetCall(url);
            var result = await response.Content.ReadAsStringAsync();
            var beneficiary =  Utilities.DeSerializeObject<Beneficiary>(result);

            return View(beneficiary);

        }
        public IActionResult AddScheme()
        {
            return View();
        }
        public IActionResult ViewSchemes()
        {
            return View();
        }
        public IActionResult AssociateSchemesBeneficiary()
        {
            return View();
        }
        public IActionResult EditBeneficiary(Beneficiary beneficiary)
        {
            return View();
        }
    }
}
