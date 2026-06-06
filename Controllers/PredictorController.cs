using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using WorldCup2026.Data;
using WorldCup2026.Models;

namespace WorldCup2026.Controllers
{
    [Authorize]
    public class PredictorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredictorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.OrderBy(t => t.GroupLetter).ToListAsync();
            return View(teams);
        }

        [HttpPost]
        public async Task<IActionResult> SavePredictions(string groupOrders)
        {
            if (string.IsNullOrEmpty(groupOrders))
            {
                return BadRequest();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            var oldPredictions = _context.GroupPredictions.Where(p => p.UserId == userId);
            _context.GroupPredictions.RemoveRange(oldPredictions);

            var oldThird = _context.ThirdPlacePredictions.Where(p => p.UserId == userId);
            _context.ThirdPlacePredictions.RemoveRange(oldThird);

            var oldKnockout = _context.KnockoutPredictions.Where(p => p.UserId == userId);
            _context.KnockoutPredictions.RemoveRange(oldKnockout);

            var predictions = JsonSerializer.Deserialize<Dictionary<string, string>>(groupOrders);

            if (predictions != null)
            {
                foreach (var entry in predictions)
                {
                    string[] teamIds = entry.Value.Split(',');
                    for (int i = 0; i < teamIds.Length; i++)
                    {
                        if (int.TryParse(teamIds[i], out int teamId))
                        {
                            _context.GroupPredictions.Add(new GroupPrediction
                            {
                                UserId = userId,
                                TeamId = teamId,
                                PredictedPosition = i + 1
                            });
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("RankThirdPlaced");
        }

        public async Task<IActionResult> RankThirdPlaced()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            var thirdPlaced = await _context.GroupPredictions
                .Where(p => p.UserId == userId && p.PredictedPosition == 3)
                .Include(p => p.Team)
                .Select(p => p.Team)
                .ToListAsync();

            return View(thirdPlaced);
        }

        [HttpPost]
        public async Task<IActionResult> SaveThirdPlacedRankings(string rankedIds)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            if (string.IsNullOrEmpty(rankedIds)) return RedirectToAction("RankThirdPlaced");

            var ids = rankedIds.Split(',').Select(int.Parse).ToList();

            var oldPredictions = _context.ThirdPlacePredictions.Where(p => p.UserId == userId);
            _context.ThirdPlacePredictions.RemoveRange(oldPredictions);

            for (int i = 0; i < ids.Count; i++)
            {
                _context.ThirdPlacePredictions.Add(new ThirdPlacePrediction
                {
                    UserId = userId,
                    TeamId = ids[i],
                    Rank = i + 1
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Bracket");
        }
    }
}