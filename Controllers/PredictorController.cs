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
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            // 1. Дефинираме цветовете ВИНАГИ, за да ги има и във формата, и в summary-то
            ViewBag.GroupColors = new Dictionary<string, string>
            {
                {"A", "#6A0dad"}, {"B", "#0052B4"}, {"C", "#00B2A9"}, {"D", "#FF4500"},
                {"E", "#007A33"}, {"F", "#E10600"}, {"G", "#9EFD38"}, {"H", "#FF1493"},
                {"I", "#38B6FF"}, {"J", "#8B0000"}, {"K", "#FFA07A"}, {"L", "#111111"}
            };

            bool isFinished = await _context.KnockoutPredictions.AnyAsync(p => p.UserId == userId && p.MatchId == 104);

            if (isFinished)
            {
                ViewBag.GroupPredictions = await _context.GroupPredictions
                    .Where(p => p.UserId == userId)
                    .Include(p => p.Team)
                    .OrderBy(p => p.Team.GroupLetter)
                    .ThenBy(p => p.PredictedPosition)
                    .ToListAsync();

                var finals = await _context.KnockoutPredictions
                    .Where(p => p.UserId == userId && (p.MatchId == 103 || p.MatchId == 104))
                    .Include(p => p.WinnerTeam)
                    .ToListAsync();

                var sfWinners = await _context.KnockoutPredictions
                    .Where(p => p.UserId == userId && (p.MatchId == 101 || p.MatchId == 102))
                    .Include(p => p.WinnerTeam)
                    .ToDictionaryAsync(p => p.MatchId, p => p.WinnerTeam);

                var finalMatch = finals.FirstOrDefault(f => f.MatchId == 104);
                var thirdPlaceMatch = finals.FirstOrDefault(f => f.MatchId == 103);

                ViewBag.Gold = finalMatch?.WinnerTeam;
                ViewBag.Silver = (sfWinners.ContainsKey(101) && sfWinners[101] == finalMatch?.WinnerTeam) ? sfWinners[102] : sfWinners[101];
                ViewBag.Bronze = thirdPlaceMatch?.WinnerTeam;
                ViewBag.IsFinished = true;
            }

            var teams = await _context.Teams.OrderBy(t => t.GroupLetter).ToListAsync();
            return View(teams);
        }

        [HttpPost]
        public async Task<IActionResult> SavePredictions(string groupOrders)
        {
            if (string.IsNullOrEmpty(groupOrders)) return BadRequest();

            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            _context.GroupPredictions.RemoveRange(_context.GroupPredictions.Where(p => p.UserId == userId));
            _context.ThirdPlacePredictions.RemoveRange(_context.ThirdPlacePredictions.Where(p => p.UserId == userId));
            _context.KnockoutPredictions.RemoveRange(_context.KnockoutPredictions.Where(p => p.UserId == userId));

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
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
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
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            if (string.IsNullOrEmpty(rankedIds)) return RedirectToAction("RankThirdPlaced");

            var ids = rankedIds.Split(',').Select(int.Parse).ToList();

            _context.ThirdPlacePredictions.RemoveRange(_context.ThirdPlacePredictions.Where(p => p.UserId == userId));

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

        public async Task<IActionResult> Reset()
        {
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            _context.GroupPredictions.RemoveRange(_context.GroupPredictions.Where(p => p.UserId == userId));
            _context.ThirdPlacePredictions.RemoveRange(_context.ThirdPlacePredictions.Where(p => p.UserId == userId));
            _context.KnockoutPredictions.RemoveRange(_context.KnockoutPredictions.Where(p => p.UserId == userId));

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}