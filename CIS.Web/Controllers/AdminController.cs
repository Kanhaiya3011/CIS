using CIS.Models.Relations;
using CIS.Models;
using CIS.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CIS.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _apiUrl = @"https://localhost:7042/api";
        private readonly IUtilities _Utilities;
        public AdminController(IUtilities utilities)
        {
            _Utilities = utilities;
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

        public async Task<IActionResult> ApproveBen(int id)
        {
            if (id == 0)
                return BadRequest();

            var url = $"{_apiUrl}/Beneficiary/{id}";
            var result = await _Utilities.HttpGetCall<Beneficiary>(url);
            result.Status = "Approved";
            
            var approve = await _Utilities.HttpPutCall<Beneficiary>(url, result);
            url = $"{_apiUrl}/Beneficiary";
            var newResult = await _Utilities.HttpGetCall<IList<Beneficiary>>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("ViewBeneficiary", newResult);
        }
        public async Task<IActionResult> RejectBen(int id)
        {
            if (id == 0)
                return BadRequest();
            var url = $"{_apiUrl}/Beneficiary/{id}";
            var result = await _Utilities.HttpGetCall<Beneficiary>(url);
            result.Status = "Reject";

            var approve = await _Utilities.HttpPutCall<Beneficiary>(url, result);
            url = $"{_apiUrl}/Beneficiary";
            var newResult = await _Utilities.HttpGetCall<IList<Beneficiary>>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("ViewBeneficiary", newResult);
        }
        public async Task<IActionResult> AddScheme()
        {
            var scheme = new SchemeViewModel();
            var url = $"{_apiUrl}/Category";
            scheme.scheme = new Scheme();
            scheme.categories = await _Utilities.HttpGetCall<IList<Category>>(url);
            TempData["LoggedInUser"] = HttpContext.Session.GetString("LoggedInUser");
            return View("AddScheme", scheme);
        }
        [HttpPost]
        public async Task<IActionResult> AddScheme(Scheme scheme)
        {
            var url = $"{_apiUrl}/Scheme";
            var newScheme = new SchemeViewModel();
            if (ModelState.IsValid)
            {
                var result = _Utilities.HttpPostCall<Scheme>(url, scheme);
                newScheme.categories = await _Utilities.HttpGetCall<IList<Category>>($"{_apiUrl}/Category");
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
            var result = await _Utilities.HttpGetCall<IList<Scheme>>(url);

            return View("ViewSchemes", result);
        }
        public async Task<IActionResult> AssociateSchemesBeneficiary()
        {
            var vw = new ViewModelBeneficiarySchemeApplied();
            vw.Beneficiary = await _Utilities.HttpGetCall<IList<Beneficiary>>($"{_apiUrl}/Beneficiary");
            vw.Schemes = await _Utilities.HttpGetCall<IList<Scheme>>($"{_apiUrl}/Beneficiary");
            vw.SchemeApplied = await _Utilities.HttpGetCall<IList<BeneficiarySchemeApplied>>($"{_apiUrl}/Beneficiary");
            return View(vw);
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
    }
}
