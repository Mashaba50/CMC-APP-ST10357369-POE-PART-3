// HRController.cs
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // View all approved claims
        public async Task<IActionResult> ApprovedClaims()
        {
            var claims = await _context.Claims
                .Where(c => c.Status == "Approved")
                .ToListAsync();

            return View(claims);
        }

        // Generate invoice/report
        public IActionResult GenerateReport()
        {
            var claims = _context.Claims
                .Where(c => c.Status == "Approved")
                .ToList();

            // Example: Generate CSV file for download
            var csv = "Lecturer Name, Email, Hours, Rate, Total\n";
            foreach (var c in claims)
            {
                csv += $"{c.LecturerName},{c.Email},{c.HoursWorked},{c.HourlyRate},{c.TotalAmount}\n";
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "ApprovedClaimsReport.csv");
        }

        // Manage lecturer data
        public async Task<IActionResult> LecturerList()
        {
            var lecturers = await _context.Lecturers.ToListAsync();
            return View(lecturers);
        }

        public async Task<IActionResult> EditLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> EditLecturer(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(lecturer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Lecturer data updated successfully.";
                return RedirectToAction("LecturerList");
            }
            return View(lecturer);
        }
    }
}
