using CMCS.Data;
using Microsoft.AspNetCore.Mvc;

public class CoordinatorController : Controller
{
    private readonly ApplicationDbContext _context;
    public CoordinatorController(ApplicationDbContext context) { _context = context; }

    public IActionResult ViewClaims()
    {
        var claims = _context.Claims.ToList();
        return View(claims);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int claimId)
    {
        var claim = await _context.Claims.FindAsync(claimId);
        if (claim != null)
        {
            // Example validation
            if (claim.HoursWorked <= 40 && claim.HourlyRate <= 1000)
                claim.Status = "Approved";
            else
                claim.Status = "Rejected";

            await _context.SaveChangesAsync();
        }
        return RedirectToAction("ViewClaims");
    }
}
