using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;
using WorldCup2026.Models;

namespace WorldCup2026.Controllers
{
    public class MatchUpdateViewModel
    {
        public int MatchId { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
    }

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
                .OrderBy(m => m.Id) // Сортираме по ID за правилния ред на фазите
                .ToListAsync();

            return View(matches);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SaveAll(List<MatchUpdateViewModel> updates)
        {
            foreach (var update in updates)
            {
                var match = await _context.Matches.FindAsync(update.MatchId);
                if (match != null)
                {
                    // Ако е null, записваме 0, иначе записваме стойноста
                    match.HomeScore = update.HomeScore ?? 0;
                    match.AwayScore = update.AwayScore ?? 0;
                    match.Status = "Finished";
                }
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "All scores updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EndGroupPhase()
        {
            var groupMatches = await _context.Matches.Where(m => m.Id <= 72).ToListAsync();
            if (groupMatches.Any(m => m.Status != "Finished"))
            {
                TempData["Error"] = "Finish all group stage matches!";
                return RedirectToAction(nameof(Index));
            }

            var teams = await _context.Teams.ToListAsync();
            var standings = teams.Select(t => new {
                Team = t,
                Points = groupMatches.Where(m => m.HomeTeamId == t.Id || m.AwayTeamId == t.Id).Sum(m => (m.HomeTeamId == t.Id && m.HomeScore > m.AwayScore) || (m.AwayTeamId == t.Id && m.AwayScore > m.HomeScore) ? 3 : (m.HomeScore == m.AwayScore ? 1 : 0)),
                GD = groupMatches.Where(m => m.HomeTeamId == t.Id).Sum(m => (m.HomeScore ?? 0) - (m.AwayScore ?? 0)) + groupMatches.Where(m => m.AwayTeamId == t.Id).Sum(m => (m.AwayScore ?? 0) - (m.HomeScore ?? 0)),
                GS = groupMatches.Where(m => m.HomeTeamId == t.Id).Sum(m => m.HomeScore ?? 0) + groupMatches.Where(m => m.AwayTeamId == t.Id).Sum(m => m.AwayScore ?? 0)
            }).ToList();

            var groupedStandings = standings.GroupBy(s => s.Team.GroupLetter).ToDictionary(g => g.Key, g => g.OrderByDescending(s => s.Points).ThenByDescending(s => s.GD).ThenByDescending(s => s.GS).ToList());
            var firsts = new Dictionary<string, Team>(); var seconds = new Dictionary<string, Team>(); var thirds = new List<dynamic>();

            foreach (var group in groupedStandings) {
                firsts[group.Key] = group.Value[0].Team;
                seconds[group.Key] = group.Value[1].Team;
                thirds.Add(group.Value[2]);
            }
            var bestThirds = thirds.OrderByDescending(t => t.Points).ThenByDescending(t => t.GD).ThenByDescending(t => t.GS).Take(8).Select(t => (Team)t.Team).ToList();

            await SetKnockoutMatch(73, firsts["A"], bestThirds[0]);
            await SetKnockoutMatch(74, seconds["A"], seconds["B"]);
            await SetKnockoutMatch(75, firsts["B"], bestThirds[1]);
            await SetKnockoutMatch(76, firsts["F"], seconds["C"]);
            await SetKnockoutMatch(77, firsts["C"], bestThirds[2]);
            await SetKnockoutMatch(78, firsts["E"], seconds["D"]);
            await SetKnockoutMatch(79, firsts["D"], bestThirds[3]);
            await SetKnockoutMatch(80, seconds["E"], seconds["F"]);
            await SetKnockoutMatch(81, firsts["I"], bestThirds[4]);
            await SetKnockoutMatch(82, seconds["H"], seconds["G"]);
            await SetKnockoutMatch(83, firsts["H"], bestThirds[5]);
            await SetKnockoutMatch(84, firsts["G"], seconds["I"]);
            await SetKnockoutMatch(85, firsts["J"], bestThirds[6]);
            await SetKnockoutMatch(86, seconds["K"], seconds["J"]);
            await SetKnockoutMatch(87, firsts["K"], bestThirds[7]);
            await SetKnockoutMatch(88, firsts["L"], seconds["L"]);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EndRO32()
        {
            var matches = await _context.Matches.Where(m => m.Id >= 73 && m.Id <= 88).Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
            if (matches.Any(m => m.Status != "Finished")) { TempData["Error"] = "Finish all RO32 matches!"; return RedirectToAction(nameof(Index)); }
            
            Team GetWinner(Match m) => m.HomeScore > m.AwayScore ? m.HomeTeam : m.AwayTeam;
            await SetKnockoutMatch(89, GetWinner(matches[0]), GetWinner(matches[1]));
            await SetKnockoutMatch(90, GetWinner(matches[2]), GetWinner(matches[3]));
            await SetKnockoutMatch(91, GetWinner(matches[4]), GetWinner(matches[5]));
            await SetKnockoutMatch(92, GetWinner(matches[6]), GetWinner(matches[7]));
            await SetKnockoutMatch(93, GetWinner(matches[8]), GetWinner(matches[9]));
            await SetKnockoutMatch(94, GetWinner(matches[10]), GetWinner(matches[11]));
            await SetKnockoutMatch(95, GetWinner(matches[12]), GetWinner(matches[13]));
            await SetKnockoutMatch(96, GetWinner(matches[14]), GetWinner(matches[15]));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EndRO16()
        {
            var matches = await _context.Matches.Where(m => m.Id >= 89 && m.Id <= 96).Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
            if (matches.Any(m => m.Status != "Finished")) { TempData["Error"] = "Finish all RO16 matches!"; return RedirectToAction(nameof(Index)); }

            Team GetWinner(Match m) => m.HomeScore > m.AwayScore ? m.HomeTeam : m.AwayTeam;
            await SetKnockoutMatch(97, GetWinner(matches[0]), GetWinner(matches[1]));
            await SetKnockoutMatch(98, GetWinner(matches[2]), GetWinner(matches[3]));
            await SetKnockoutMatch(99, GetWinner(matches[4]), GetWinner(matches[5]));
            await SetKnockoutMatch(100, GetWinner(matches[6]), GetWinner(matches[7]));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EndQuarterFinals()
        {
            var matches = await _context.Matches.Where(m => m.Id >= 97 && m.Id <= 100).Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
            if (matches.Any(m => m.Status != "Finished")) { TempData["Error"] = "Finish all QF matches!"; return RedirectToAction(nameof(Index)); }

            Team GetWinner(Match m) => m.HomeScore > m.AwayScore ? m.HomeTeam : m.AwayTeam;
            await SetKnockoutMatch(101, GetWinner(matches[0]), GetWinner(matches[1]));
            await SetKnockoutMatch(102, GetWinner(matches[2]), GetWinner(matches[3]));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EndSemiFinals()
        {
            var matches = await _context.Matches.Where(m => m.Id >= 101 && m.Id <= 102).Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
            if (matches.Any(m => m.Status != "Finished")) { TempData["Error"] = "Finish all SF matches!"; return RedirectToAction(nameof(Index)); }

            Team GetWinner(Match m) => m.HomeScore > m.AwayScore ? m.HomeTeam : m.AwayTeam;
            Team GetLoser(Match m) => m.HomeScore > m.AwayScore ? m.AwayTeam : m.HomeTeam;

            await SetKnockoutMatch(103, GetLoser(matches[0]), GetLoser(matches[1])); // 3-то място
            await SetKnockoutMatch(104, GetWinner(matches[0]), GetWinner(matches[1])); // Финал

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
[HttpPost]
public async Task<IActionResult> EndFinals()
{
    var finals = await _context.Matches.Where(m => m.Id >= 103 && m.Id <= 104).ToListAsync();
    
    // Проверка дали мачовете са изиграни
    if (finals.Any(m => m.HomeScore == null || m.AwayScore == null))
    {
        TempData["Error"] = "Please enter scores for Finals and 3rd place match first!";
        return RedirectToAction(nameof(Index));
    }

    foreach (var match in finals)
    {
        match.Status = "Finished";
    }

    await _context.SaveChangesAsync();
    TempData["Success"] = "Tournament successfully concluded! See the Final Standings.";
    return RedirectToAction("Index", "Standings"); // Пренасочваме директно към подиума
}

        private async Task SetKnockoutMatch(int matchId, Team home, Team away)
{
    var m = await _context.Matches.FindAsync(matchId);
    
    if (m != null) 
    { 
        // Ако мачът съществува, просто го обновяваме
        m.HomeTeamId = home.Id; 
        m.AwayTeamId = away.Id; 
        m.Status = "Scheduled"; 
    }
    else 
    {
        // Ако мачът НЕ съществува, използваме суров SQL, за да вмъкнем ID-то ръчно
        string sql = $"SET IDENTITY_INSERT Matches ON; " +
                     $"INSERT INTO Matches (Id, HomeTeamId, AwayTeamId, StadiumId, KickOffTime, Status) " +
                     $"VALUES ({matchId}, {home.Id}, {away.Id}, 1, '2026-07-04 18:00:00', 'Scheduled'); " +
                     $"SET IDENTITY_INSERT Matches OFF;";
        
        await _context.Database.ExecuteSqlRawAsync(sql);
    }
}

[Authorize(Roles = "Admin")]
public async Task<IActionResult> ResetGroupMatches()
{
    // 1. Изтриваме грешните мачове
    var matchesToRemove = await _context.Matches.Where(m => m.Id >= 37 && m.Id <= 48).ToListAsync();
    _context.Matches.RemoveRange(matchesToRemove);
    await _context.SaveChangesAsync();

    // 2. Пресъздаваме ги на чисто с правилните отбори
    // Имена на отборите по ID от твоите снимки:
    // G: 27(Bel), 28(Egy), 31(Ira), 32(Nze)
    // H: 25(Spa), 26(Cap), 29(Sau), 30(Uru)
    
    var newMatches = new List<Match>
    {
        // Група G
        new Match { Id = 37, HomeTeamId = 27, AwayTeamId = 28, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 38, HomeTeamId = 31, AwayTeamId = 32, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 39, HomeTeamId = 27, AwayTeamId = 31, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 40, HomeTeamId = 28, AwayTeamId = 32, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 41, HomeTeamId = 27, AwayTeamId = 32, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 42, HomeTeamId = 28, AwayTeamId = 31, Status = "Scheduled", KickOffTime = DateTime.Now },
        // Група H
        new Match { Id = 43, HomeTeamId = 25, AwayTeamId = 26, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 44, HomeTeamId = 29, AwayTeamId = 30, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 45, HomeTeamId = 25, AwayTeamId = 29, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 46, HomeTeamId = 26, AwayTeamId = 30, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 47, HomeTeamId = 25, AwayTeamId = 30, Status = "Scheduled", KickOffTime = DateTime.Now },
        new Match { Id = 48, HomeTeamId = 26, AwayTeamId = 29, Status = "Scheduled", KickOffTime = DateTime.Now }
    };

    _context.Matches.AddRange(newMatches);
    await _context.SaveChangesAsync();

    TempData["Success"] = "Group G and H matches have been reset and fixed!";
    return RedirectToAction(nameof(Index));
}
    }
}