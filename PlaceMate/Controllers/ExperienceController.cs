using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaceMate.Data;
using PlaceMate.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlaceMate.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, string jobRole, int? year, int? companyId)
        {
            var experiencesQuery = _context.InterviewExperiences
                                           .Include(e => e.Company)
                                           .Include(e => e.AppUser)
                                           .Where(e => e.IsApproved);

            //search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                experiencesQuery = experiencesQuery.Where(e =>
                    e.JobRole.Contains(searchTerm) ||
                    e.TechnicalQuestions.Contains(searchTerm) ||
                    e.HRQuestions.Contains(searchTerm) ||
                    (e.TipsForJuniors != null && e.TipsForJuniors.Contains(searchTerm))
                );
            }

            //role
            if (!string.IsNullOrEmpty(jobRole))
            {
                experiencesQuery = experiencesQuery.Where(e => e.JobRole == jobRole);
            }

            // year
            if (year.HasValue)
            {
                experiencesQuery = experiencesQuery.Where(e => e.YearOfInterview == year.Value);
            }

            // Company
            if (companyId.HasValue)
            {
                experiencesQuery = experiencesQuery.Where(e => e.CompanyId == companyId.Value);
            }

            var filteredExperiences = await experiencesQuery.ToListAsync();

            // --- Prepare data for all dropdowns ---
            ViewBag.JobRoles = await _context.InterviewExperiences.Where(e => e.IsApproved).Select(e => e.JobRole).Distinct().ToListAsync();
            ViewBag.Years = await _context.InterviewExperiences.Where(e => e.IsApproved).Select(e => e.YearOfInterview).Distinct().OrderByDescending(y => y).ToListAsync();
            ViewBag.Companies = await _context.Companies.OrderBy(c => c.CompanyName).ToListAsync();

            return View(filteredExperiences);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var companies = await _context.Companies.OrderBy(c => c.CompanyName).ToListAsync();
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName");

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InterviewExperience experience)
        {
            experience.IsApproved = false;
            experience.AppUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            var companies = await _context.Companies.OrderBy(c => c.CompanyName).ToListAsync();
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName", experience.CompanyId);

            return View(experience);
        }
    }
}