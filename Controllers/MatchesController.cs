using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public async Task<IActionResult> UpdateScore(int matchId, int? homeScore, int? awayScore, string submitAction)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match == null)
            {
                return NotFound();
            }

            if (submitAction == "reset")
            {
                match.HomeScore = null;
                match.AwayScore = null;
                match.Status = "Scheduled";
            }
            else if (submitAction == "confirm")
            {
                match.HomeScore = homeScore ?? 0;
                match.AwayScore = awayScore ?? 0;
                match.Status = "Finished";
            }

            _context.Update(match);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}