using CMCS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CMCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) { _context = context; }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string lecturerName, string lecturerEmail)
        {
            if (string.IsNullOrEmpty(lecturerName) || string.IsNullOrEmpty(lecturerEmail))
            {
                ViewBag.Error = "Please enter both name and email.";
                return View();
            }

            HttpContext.Session.SetString("LecturerName", lecturerName);
            HttpContext.Session.SetString("LecturerEmail", lecturerEmail);

            return RedirectToAction("SubmitClaim", "Lecturer");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
