using CIS.Models;
using CIS.Models.Relations;
using CIS.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Policy;

namespace CIS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly string _apiUrl = @"https://localhost:7042/api";

        public async Task<IActionResult> Index(int id)
        {
            //var id = Request.Query["id"];
            if (id != 0)
            {
                var url = $"{_apiUrl}/User/{id}";
                var result = await Utilities.HttpGetCall<User>(url);
                
                TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
                return View("Index", result);
            }
            else
                return BadRequest();
           
        }
        public async Task<IActionResult> ViewBeneficiary()
        {
            var url = $"{_apiUrl}/Beneficiary";
            var result = await Utilities.HttpGetCall<IList<Beneficiary>>(url);
            return View("ViewBeneficiary", result);
        }
        public IActionResult AddBeneficiary()
        {
            var vw = new Beneficiary();
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View(vw);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(Beneficiary beneficiary)
        {
            var ben = new Beneficiary();
            int? id = 0;
            if (ModelState.IsValid)
            {
                var url = $"{_apiUrl}/Beneficiary";
                beneficiary.Status = "Pending";
                beneficiary.IsActive = false;
                var result = await Utilities.HttpPostCall<Beneficiary>(url, beneficiary);
                id = result?.Id;
            }
            TempData["benCreated"] = $"Successfully created User with the ID : {id}";
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("AddBeneficiary");
        }
        public async Task<IActionResult> ViewBen(int id)
        {
            if (id == 0)
                return BadRequest();
            var url = $"{_apiUrl}/Beneficiary/{id}";
            var result = await Utilities.HttpGetCall<Beneficiary>(url);
            return View("ViewBen", result );

        }
        public async Task<IActionResult> EditBen(int id)
        {
            if (id == 0)
                return BadRequest();
            var url = $"{_apiUrl}/Beneficiary/{id}";
            var result = await Utilities.HttpGetCall<Beneficiary>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("EditBen", result);
        }
        [HttpPost]
        public async Task<IActionResult> EditBen(Beneficiary ben)
        {
            if (ben.Id == 0)
                return BadRequest();

            var url = $"{_apiUrl}/Beneficiary";
            var result = await Utilities.HttpPutCall<Beneficiary>(url, ben);

            var newBen = new Beneficiary();
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("EditBen", newBen);
        }
        public async Task<IActionResult> AddScheme()
        {
            var scheme = new SchemeViewModel();
            var url = $"{_apiUrl}/Category";
            scheme.scheme = new Scheme();
            scheme.categories = await Utilities.HttpGetCall<IList<Category>>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("AddScheme", scheme);
        }
        [HttpPost]
        public async Task<IActionResult> AddScheme(Scheme scheme)
        {
            var url = $"{_apiUrl}/Scheme";
            var newScheme = new SchemeViewModel();
            if(ModelState.IsValid)
            {
                var result = Utilities.HttpPostCall<Scheme>(url, scheme);  
                newScheme.categories = await Utilities.HttpGetCall<IList<Category>>($"{_apiUrl}/Category");
            }
            else
            {
                return BadRequest();
            }
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("AddScheme", newScheme);
        }
        public async Task<IActionResult> ViewSchemesAsync()
        {
            var url = $"{_apiUrl}/Scheme";
            var result = await Utilities.HttpGetCall<IList<Scheme>>(url);

            return View("ViewSchemes", result);
        }
        public async Task<IActionResult> AssociateSchemesBeneficiary()
        {
            var vw = new ViewModelBeneficiarySchemeApplied();
            vw.Beneficiary =await Utilities.HttpGetCall<IList<Beneficiary>>($"{_apiUrl}/Beneficiary");
            vw.Schemes = await Utilities.HttpGetCall<IList<Scheme>>($"{_apiUrl}/Beneficiary");
            vw.SchemeApplied = await Utilities.HttpGetCall<IList<BeneficiarySchemeApplied>>($"{_apiUrl}/Beneficiary");
            return View(vw);
        }
        public async Task<IActionResult> EditScheme(int id)
        {
            var url = $"{_apiUrl}/Scheme/{id}";
            var result = await Utilities.HttpGetCall<Scheme>(url);

            return View("EditScheme", result);

        }
        public async Task<IActionResult> DetailsScheme(int id)
        {
            var url = $"{_apiUrl}/Scheme/{id}";
            var result = await Utilities.HttpGetCall<Scheme>(url);

            return View("DetailsScheme", result);

        }
    }
}
