using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;
using WorldCup2026.Models;

namespace WorldCup2026.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .OrderBy(m => m.KickOffTime)
                .ToListAsync();

            return View(matches);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, int? homeScore, int? awayScore)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();

            match.HomeScore = homeScore;
            match.AwayScore = awayScore;

            if (homeScore.HasValue && awayScore.HasValue)
            {
                match.Status = "Finished";
            }
            else
            {
                match.Status = "Scheduled";
            }

            _context.Update(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}