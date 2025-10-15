using Microsoft.AspNetCore.Authorization; // Added for role-based protection
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceMate.Data;
using PlaceMate.Models;
using PlaceMate.ViewModels; // ✅ Added for Dashboard ViewModel
using System.Linq;
using System.Threading.Tasks;

namespace PlaceMate.Controllers
{
    [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access this controller
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================
        // 🔹 EXPERIENCE MODERATION
        // ============================

        public async Task<IActionResult> ModerationQueue()
        {
            var unapprovedExperiences = await _context.InterviewExperiences
                .Include(e => e.Company)
                .Include(e => e.AppUser)
                .Where(e => !e.IsApproved)
                .ToListAsync();

            return View(unapprovedExperiences);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveExperience(int? id)
        {
            if (id == null)
                return NotFound();

            var experience = await _context.InterviewExperiences.FindAsync(id);
            if (experience == null)
                return NotFound();

            experience.IsApproved = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ModerationQueue));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectExperience(int? id)
        {
            if (id == null)
                return NotFound();

            var experience = await _context.InterviewExperiences.FindAsync(id);
            if (experience == null)
                return NotFound();

            _context.InterviewExperiences.Remove(experience);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ModerationQueue));
        }

        // ============================
        // 🔹 ADMIN DASHBOARD (UPDATED)
        // ============================

        // GET: /Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalExperiences = await _context.InterviewExperiences.CountAsync(e => e.IsApproved),
                PendingExperiences = await _context.InterviewExperiences.CountAsync(e => !e.IsApproved),
                TotalCompanies = await _context.Companies.CountAsync(),
                TotalStudents = await _context.AppUsers.CountAsync(u => u.Role == "Student"),

                // Top 5 companies by number of submitted experiences
                TopCompanies = await _context.InterviewExperiences
                    .Include(e => e.Company)
                    .GroupBy(e => e.Company.CompanyName)
                    .Select(g => new { CompanyName = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToDictionaryAsync(k => k.CompanyName, v => v.Count)
            };

            return View(viewModel);
        }

        // ============================
        // 🔹 USER CREATION
        // ============================

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(string email, string password, string role)
        {
            if (_context.AppUsers.Any(u => u.Email == email))
            {
                TempData["ErrorMessage"] = "A user with this email already exists.";
                return RedirectToAction("CreateUser");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new AppUser
            {
                Email = email,
                PasswordHash = passwordHash,
                Role = role
            };

            _context.AppUsers.Add(newUser);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction("Dashboard");
        }

        // ============================
        // 🔹 COMPANY MANAGEMENT
        // ============================

        public async Task<IActionResult> ManageCompanies()
        {
            var allCompanies = await _context.Companies.ToListAsync();
            return View(allCompanies);
        }

        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageCompanies");
            }

            return View(company);
        }

        // ============================
        // 🔹 EDIT COMPANY
        // ============================

        public async Task<IActionResult> EditCompany(int? id)
        {
            if (id == null)
                return NotFound();

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(int id, Company company)
        {
            if (id != company.CompanyId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Companies.Any(e => e.CompanyId == company.CompanyId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(ManageCompanies));
            }
            return View(company);
        }

        // ============================
        // 🔹 DELETE COMPANY
        // ============================

        public async Task<IActionResult> DeleteCompany(int? id)
        {
            if (id == null)
                return NotFound();

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost, ActionName("DeleteCompany")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompanyConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return NotFound();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageCompanies));
        }
    }
}
