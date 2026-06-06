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
    public class BracketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BracketController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string stage = "R32")
        {
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            ViewBag.Stage = stage;
            var matches = await GetMatchesForStage(stage, userId);

            return View(matches);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStage(string stage, string winnersJson)
        {
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

            if (string.IsNullOrEmpty(winnersJson)) return RedirectToAction("Index", new { stage = stage });

            using (JsonDocument doc = JsonDocument.Parse(winnersJson))
            {
                var root = doc.RootElement;
                var old = _context.KnockoutPredictions.Where(p => p.UserId == userId && p.Stage == stage);
                _context.KnockoutPredictions.RemoveRange(old);

                foreach (var property in root.EnumerateObject())
                {
                    if (int.TryParse(property.Name, out int matchId))
                    {
                        string valStr = property.Value.ValueKind == JsonValueKind.String
                                        ? property.Value.GetString()
                                        : property.Value.GetRawText();

                        if (int.TryParse(valStr, out int winnerTeamId))
                        {
                            _context.KnockoutPredictions.Add(new KnockoutPrediction
                            {
                                UserId = userId,
                                MatchId = matchId,
                                WinnerTeamId = winnerTeamId,
                                Stage = stage
                            });
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            if (stage == "Final") return RedirectToAction("Results", "Bracket");

            string nextStage = stage switch
            {
                "R32" => "R16",
                "R16" => "QF",
                "QF" => "SF",
                "SF" => "Final",
                _ => "Index"
            };

            return RedirectToAction("Index", new { stage = nextStage });
        }

        public async Task<IActionResult> Results()
        {
            var userIdClaim = User.FindFirst("UserId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim.Value);

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

            if (finalMatch == null || thirdPlaceMatch == null) return RedirectToAction("Index", "Bracket");

            var gold = finalMatch.WinnerTeam;
            var silver = (sfWinners.ContainsKey(101) && sfWinners[101] == gold) ? sfWinners[102] : sfWinners[101];
            var bronze = thirdPlaceMatch.WinnerTeam;

            ViewBag.Gold = gold;
            ViewBag.Silver = silver;
            ViewBag.Bronze = bronze;

            return View();
        }

        private async Task<List<MatchViewModel>> GetMatchesForStage(string stage, int userId)
{
    var matches = new List<MatchViewModel>();
    var usedTeamIds = new HashSet<int>();

    Team GetSafeTeam(Team team)
    {
        if (team == null || usedTeamIds.Contains(team.Id)) return null;
        usedTeamIds.Add(team.Id);
        return team;
    }

    var groupPredictions = await _context.GroupPredictions
        .Where(p => p.UserId == userId)
        .Include(p => p.Team)
        .ToListAsync();

    var thirdPlaces = await _context.ThirdPlacePredictions
        .Where(p => p.UserId == userId)
        .OrderBy(p => p.Rank)
        .Include(p => p.Team)
        .ToListAsync();

    var qualifiedTeamIds = groupPredictions
        .Where(p => p.PredictedPosition == 1 || p.PredictedPosition == 2)
        .Select(p => p.TeamId)
        .ToHashSet();

    var validThirdPlaces = thirdPlaces
        .Where(t => !qualifiedTeamIds.Contains(t.TeamId))
        .ToList();

    Team GetTeam(string group, int pos) =>
        groupPredictions.FirstOrDefault(p => p.Team.GroupLetter == group && p.PredictedPosition == pos)?.Team;

    if (stage == "R32")
    {
        Team GetThird(int rankIndex) => rankIndex < validThirdPlaces.Count ? validThirdPlaces[rankIndex].Team : null;

        matches.Add(new MatchViewModel { Id = 73, Home = GetSafeTeam(GetTeam("A", 1)), Away = GetSafeTeam(GetThird(0)) });
        matches.Add(new MatchViewModel { Id = 74, Home = GetSafeTeam(GetTeam("A", 2)), Away = GetSafeTeam(GetTeam("B", 2)) });
        matches.Add(new MatchViewModel { Id = 75, Home = GetSafeTeam(GetTeam("B", 1)), Away = GetSafeTeam(GetThird(1)) });
        matches.Add(new MatchViewModel { Id = 76, Home = GetSafeTeam(GetTeam("F", 1)), Away = GetSafeTeam(GetTeam("C", 2)) });
        matches.Add(new MatchViewModel { Id = 77, Home = GetSafeTeam(GetTeam("C", 1)), Away = GetSafeTeam(GetThird(2)) });
        matches.Add(new MatchViewModel { Id = 78, Home = GetSafeTeam(GetTeam("E", 1)), Away = GetSafeTeam(GetTeam("D", 2)) });
        matches.Add(new MatchViewModel { Id = 79, Home = GetSafeTeam(GetTeam("D", 1)), Away = GetSafeTeam(GetThird(3)) });
        matches.Add(new MatchViewModel { Id = 80, Home = GetSafeTeam(GetTeam("E", 2)), Away = GetSafeTeam(GetTeam("F", 2)) });
        matches.Add(new MatchViewModel { Id = 81, Home = GetSafeTeam(GetTeam("I", 1)), Away = GetSafeTeam(GetThird(4)) });
        matches.Add(new MatchViewModel { Id = 82, Home = GetSafeTeam(GetTeam("H", 2)), Away = GetSafeTeam(GetTeam("G", 2)) });
        matches.Add(new MatchViewModel { Id = 83, Home = GetSafeTeam(GetTeam("H", 1)), Away = GetSafeTeam(GetThird(5)) });
        matches.Add(new MatchViewModel { Id = 84, Home = GetSafeTeam(GetTeam("G", 1)), Away = GetSafeTeam(GetTeam("I", 2)) });
        matches.Add(new MatchViewModel { Id = 85, Home = GetSafeTeam(GetTeam("J", 1)), Away = GetSafeTeam(GetThird(6)) });
        matches.Add(new MatchViewModel { Id = 86, Home = GetSafeTeam(GetTeam("K", 2)), Away = GetSafeTeam(GetTeam("J", 2)) });
        matches.Add(new MatchViewModel { Id = 87, Home = GetSafeTeam(GetTeam("K", 1)), Away = GetSafeTeam(GetThird(7)) });
        matches.Add(new MatchViewModel { Id = 88, Home = GetSafeTeam(GetTeam("L", 1)), Away = GetSafeTeam(GetTeam("L", 2)) });
    }
    else if (stage == "R16")
    {
        var r32Winners = await _context.KnockoutPredictions.Where(p => p.UserId == userId && p.Stage == "R32").Include(p => p.WinnerTeam).ToDictionaryAsync(p => p.MatchId, p => p.WinnerTeam);
        Team GetWinner(int matchId) => r32Winners.ContainsKey(matchId) ? r32Winners[matchId] : null;

        matches.Add(new MatchViewModel { Id = 89, Home = GetSafeTeam(GetWinner(73)), Away = GetSafeTeam(GetWinner(74)) });
        matches.Add(new MatchViewModel { Id = 90, Home = GetSafeTeam(GetWinner(75)), Away = GetSafeTeam(GetWinner(76)) });
        matches.Add(new MatchViewModel { Id = 91, Home = GetSafeTeam(GetWinner(77)), Away = GetSafeTeam(GetWinner(78)) });
        matches.Add(new MatchViewModel { Id = 92, Home = GetSafeTeam(GetWinner(79)), Away = GetSafeTeam(GetWinner(80)) });
        matches.Add(new MatchViewModel { Id = 93, Home = GetSafeTeam(GetWinner(81)), Away = GetSafeTeam(GetWinner(82)) });
        matches.Add(new MatchViewModel { Id = 94, Home = GetSafeTeam(GetWinner(83)), Away = GetSafeTeam(GetWinner(84)) });
        matches.Add(new MatchViewModel { Id = 95, Home = GetSafeTeam(GetWinner(85)), Away = GetSafeTeam(GetWinner(86)) });
        matches.Add(new MatchViewModel { Id = 96, Home = GetSafeTeam(GetWinner(87)), Away = GetSafeTeam(GetWinner(88)) });
    }
    else if (stage == "QF")
    {
        var r16Winners = await _context.KnockoutPredictions.Where(p => p.UserId == userId && p.Stage == "R16").Include(p => p.WinnerTeam).ToDictionaryAsync(p => p.MatchId, p => p.WinnerTeam);
        Team GetWinner(int matchId) => r16Winners.ContainsKey(matchId) ? r16Winners[matchId] : null;

        matches.Add(new MatchViewModel { Id = 97, Home = GetSafeTeam(GetWinner(89)), Away = GetSafeTeam(GetWinner(90)) });
        matches.Add(new MatchViewModel { Id = 98, Home = GetSafeTeam(GetWinner(91)), Away = GetSafeTeam(GetWinner(92)) });
        matches.Add(new MatchViewModel { Id = 99, Home = GetSafeTeam(GetWinner(93)), Away = GetSafeTeam(GetWinner(94)) });
        matches.Add(new MatchViewModel { Id = 100, Home = GetSafeTeam(GetWinner(95)), Away = GetSafeTeam(GetWinner(96)) });
    }
    else if (stage == "SF")
    {
        var qfWinners = await _context.KnockoutPredictions.Where(p => p.UserId == userId && p.Stage == "QF").Include(p => p.WinnerTeam).ToDictionaryAsync(p => p.MatchId, p => p.WinnerTeam);
        Team GetWinner(int matchId) => qfWinners.ContainsKey(matchId) ? qfWinners[matchId] : null;

        matches.Add(new MatchViewModel { Id = 101, Home = GetSafeTeam(GetWinner(97)), Away = GetSafeTeam(GetWinner(98)) });
        matches.Add(new MatchViewModel { Id = 102, Home = GetSafeTeam(GetWinner(99)), Away = GetSafeTeam(GetWinner(100)) });
    }
    else if (stage == "Final")
    {
        var sfPredictions = await _context.KnockoutPredictions.Where(p => p.UserId == userId && (p.MatchId == 101 || p.MatchId == 102)).Include(p => p.WinnerTeam).ToListAsync();
        var qfWinners = await _context.KnockoutPredictions.Where(p => p.UserId == userId && (p.MatchId >= 97 && p.MatchId <= 100)).Include(p => p.WinnerTeam).ToDictionaryAsync(p => p.MatchId, p => p.WinnerTeam);

        Team sf1Winner = sfPredictions.FirstOrDefault(x => x.MatchId == 101)?.WinnerTeam;
        Team sf2Winner = sfPredictions.FirstOrDefault(x => x.MatchId == 102)?.WinnerTeam;

        // Логика за губещите (ако 97 е победител в 101, губещ е 98)
        Team sf1Loser = (qfWinners.ContainsKey(97) && qfWinners[97] == sf1Winner) ? qfWinners[98] : qfWinners[97];
        Team sf2Loser = (qfWinners.ContainsKey(99) && qfWinners[99] == sf2Winner) ? qfWinners[100] : qfWinners[99];

        matches.Add(new MatchViewModel { Id = 103, Home = GetSafeTeam(sf1Loser), Away = GetSafeTeam(sf2Loser) });
        matches.Add(new MatchViewModel { Id = 104, Home = GetSafeTeam(sf1Winner), Away = GetSafeTeam(sf2Winner) });
    }

    return matches;
}
    }
}