using ClaimsProcessing.Data;
using ClaimsProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaimsProcessing.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims.AsNoTracking().ToListAsync();
            return View(claims);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var claim = await _context
                .Claims.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClaimId == id);
            if (claim == null)
                return NotFound();
            return View(claim);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PolicyNumber,ClaimType,Description,ClaimAmount,Status,AgentId")] Claim claim
        )
        {
            if (!ModelState.IsValid)
                return View(claim);
            claim.CreatedAt = DateTime.UtcNow;
            claim.UpdatedAt = DateTime.UtcNow;
            _context.Add(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
                return NotFound();
            return View(claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "ClaimId,PolicyNumber,ClaimType,Description,ClaimAmount,Status,AgentId,CreatedAt"
            )]
                Claim claim
        )
        {
            if (id != claim.ClaimId)
                return NotFound();
            if (!ModelState.IsValid)
                return View(claim);

            try
            {
                claim.UpdatedAt = DateTime.UtcNow;
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Claims.AnyAsync(e => e.ClaimId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var claim = await _context
                .Claims.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClaimId == id);
            if (claim == null)
                return NotFound();
            return View(claim);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
