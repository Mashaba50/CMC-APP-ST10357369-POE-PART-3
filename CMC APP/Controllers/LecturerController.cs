using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LecturerController(ApplicationDbContext context) { _context = context; }

        private bool IsLoggedIn() => !string.IsNullOrEmpty(HttpContext.Session.GetString("LecturerEmail"));

        // Dashboard or home page after login
        public IActionResult Dashboard()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");
            ViewBag.LecturerName = HttpContext.Session.GetString("LecturerName");
            return View();
        }

        // Submit a new claim
        public IActionResult SubmitClaim()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");

            var claim = new Claim
            {
                LecturerName = HttpContext.Session.GetString("LecturerName"),
                Email = HttpContext.Session.GetString("LecturerEmail")
            };
            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile SupportingDocument)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                if (SupportingDocument != null)
                {
                    var uploads = Path.Combine("wwwroot/uploads");
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                    var filePath = Path.Combine(uploads, SupportingDocument.FileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await SupportingDocument.CopyToAsync(stream);

                    claim.SupportingDocumentPath = "/uploads/" + SupportingDocument.FileName;
                }

                claim.TotalAmount = claim.HoursWorked * claim.HourlyRate; // Auto-calc TotalAmount
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewClaims"); // Redirect after submit
            }

            return View(claim);
        }


        // View all claims submitted by this lecturer
        public IActionResult ViewClaims()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Home");

            var email = HttpContext.Session.GetString("LecturerEmail");
            var claims = _context.Claims.Where(c => c.Email == email).ToList();
            return View(claims);
        }

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}
