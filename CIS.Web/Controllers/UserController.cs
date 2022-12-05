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
        private readonly IUtilities _Utilities;
        public UserController(IUtilities utilities)
        {
            _Utilities = utilities;
        }
        public async Task<IActionResult> ViewUsers()
        {
            var url = $"{_apiUrl}/User";
            var users = await _Utilities.HttpGetCall<IList<User>>(url);
            return View(users);
        }
        public  IActionResult AddUser()
        {
            var user = new User();
            return View(user);

        }
        public async Task<IActionResult> Index(int id)
        {
            //var id = Request.Query["id"];
            if (id != 0)
            {
                var url = $"{_apiUrl}/User/{id}";
                var result = await _Utilities.HttpGetCall<User>(url);

                TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
                return View("Index", result);
            }
            else
                return BadRequest();

        }
        public async Task<IActionResult> ViewBeneficiary()
        {
            var url = $"{_apiUrl}/Beneficiary";
            var result = await _Utilities.HttpGetCall<IList<Beneficiary>>(url);
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
                var result = await _Utilities.HttpPostCall<Beneficiary>(url, beneficiary);
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
            var result = await _Utilities.HttpGetCall<Beneficiary>(url);
            return View("ViewBen", result);

        }
        public async Task<IActionResult> SchemeApplied(int val)
        {
            var id = Request.Query["val"];
            var vw = new ViewModelBeneficiarySchemeApplied();
            var url = $"{_apiUrl}/SchemesApplied/{val}";
            vw.Schemes = await _Utilities.HttpGetCall<IList<Scheme>>(url);
            vw.Schemes = vw.Schemes.Where(s => s.IsActive == true).ToList();
           
            return PartialView(vw);
        }
        public async Task<IActionResult> EditBen(int id)
        {
            if (id == 0)
                return BadRequest();
            var url = $"{_apiUrl}/Beneficiary/{id}";
            var result = await _Utilities.HttpGetCall<Beneficiary>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("EditBen", result);
        }
        [HttpPost]
        public async Task<IActionResult> EditBen(Beneficiary ben)
        {
            if (ben.Id == 0)
                return BadRequest();

            var url = $"{_apiUrl}/Beneficiary/{ben.Id}";
            var result = await _Utilities.HttpPutCall<Beneficiary>(url, ben);

            var newBen = new Beneficiary();
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("EditBen", newBen);
        }
        public async Task<IActionResult> AddScheme()
        {
            var scheme = new SchemeViewModel();
            var url = $"{_apiUrl}/Category";
            scheme.scheme = new Scheme();
            scheme.categories = await _Utilities.HttpGetCall<IList<Category>>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View(scheme);
        }
        [HttpPost]
        public async Task<IActionResult> AddScheme(Scheme scheme)
        {
            var url = $"{_apiUrl}/Scheme";
            var newScheme = new SchemeViewModel();
            if (ModelState.IsValid)
            {
               // scheme.Category = await _Utilities.HttpGetCall($"{}")
                var result = await _Utilities.HttpPostCall<Scheme>(url, scheme);
                newScheme.categories = await _Utilities.HttpGetCall<IList<Category>>($"{_apiUrl}/Category");
            }
            else
            {
                return BadRequest();
            }
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return RedirectToAction("ViewSchemes", "User");
        }
        public async Task<IActionResult> ViewSchemes()
        {
            var url = $"{_apiUrl}/Scheme";
            var result = await _Utilities.HttpGetCall<IList<Scheme>>(url);

            return View("ViewSchemes", result);
        }
        public async Task<IActionResult> AssociateSchemesBeneficiary()
        {
            var vw = new ViewModelBeneficiarySchemeApplied();
            vw.Beneficiary = await _Utilities.HttpGetCall<IList<Beneficiary>>($"{_apiUrl}/Beneficiary");
            vw.Schemes = await _Utilities.HttpGetCall<IList<Scheme>>($"{_apiUrl}/Scheme");
            vw.Schemes = vw.Schemes.Where(s => s.IsActive == true).ToList();
            vw.SchemeApplied = await _Utilities.HttpGetCall<IList<BeneficiarySchemeApplied>>($"{_apiUrl}/SchemesApplied");
            
            foreach (var ben in vw.Beneficiary)
            {
                var schemes= vw.SchemeApplied.Where(sa => sa.Beneficiary == ben.Id).ToList();
                var appliedSchemesNames = schemes.Join(vw.Schemes,
                sc => sc.Scheme,
                s => s.Id,
                (scApplied, Scheme) => new { scApplied, Scheme }).Select(c => c.Scheme.SchemeName).ToList();
                if(appliedSchemesNames.Count > 0)
                    vw.Association.Associated.Add($"{ben.FirstName} {ben.LastName}", appliedSchemesNames);
            }
            return View(vw);
        }
        [HttpPost]
        public async Task<IActionResult> EditScheme(Scheme scheme)
        {
            var url = $"{_apiUrl}/Scheme/{scheme.Id}";
            var result = await _Utilities.HttpPutCall<Scheme>(url, scheme);

            return RedirectToAction("ViewSchemes", "User");

        }
        public async Task<IActionResult> EditScheme(int id)
        {
            var url = $"{_apiUrl}/Scheme/{id}";
            var result = await _Utilities.HttpGetCall<Scheme>(url);

            return View("EditScheme", result);

        }
        
        public async Task<IActionResult> DetailsScheme(int id)
        {
            var url = $"{_apiUrl}/Scheme/{id}";
            var result = await _Utilities.HttpGetCall<Scheme>(url);

            return View("DetailsScheme", result);

        }
        [HttpPost]
        public async Task<IActionResult> AssociateSchemesBeneficiary(int Beneficiary, int Schemes)
        {
            var beneficiary = await _Utilities.HttpGetCall<Beneficiary>($"{_apiUrl}/Beneficiary/{Beneficiary}");
            var scheme = await _Utilities.HttpGetCall<Scheme>($"{_apiUrl}/Scheme/{Schemes}");

            var postData = new BeneficiarySchemeApplied
            {
                Beneficiary = beneficiary.Id,
                ApplicationStatus = "PENDING",
                IsDeleted = false,
                Scheme = scheme.Id
            };
            var url = $"{_apiUrl}/SchemesApplied";
            var result = await _Utilities.HttpPostCall<BeneficiarySchemeApplied>(url, postData);
            return RedirectToAction("AssociateSchemesBeneficiary", "User");
        }
    }
}
