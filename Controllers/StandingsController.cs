using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;
using WorldCup2026.Models;

namespace WorldCup2026.Controllers
{
    public class StandingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.ToListAsync();
            var matches = await _context.Matches.Where(m => m.Status == "Finished").ToListAsync();

            var standingsMap = teams.ToDictionary(
                t => t.Id,
                t => new TeamStandingViewModel { Team = t, Points = 0, GoalDifference = 0 }
            );

            foreach (var match in matches)
            {
                if (!match.HomeScore.HasValue || !match.AwayScore.HasValue) continue;

                var homeId = match.HomeTeamId;
                var awayId = match.AwayTeamId;
                var homeScore = match.HomeScore.Value;
                var awayScore = match.AwayScore.Value;

                standingsMap[homeId].GoalDifference += (homeScore - awayScore);
                standingsMap[awayId].GoalDifference += (awayScore - homeScore);

                if (homeScore > awayScore)
                {
                    standingsMap[homeId].Points += 3;
                }
                else if (awayScore > homeScore)
                {
                    standingsMap[awayId].Points += 3;
                }
                else
                {
                    standingsMap[homeId].Points += 1;
                    standingsMap[awayId].Points += 1;
                }
            }

            var groupedStandings = standingsMap.Values
                .GroupBy(s => s.Team.GroupLetter)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(s => s.Points)
                          .ThenByDescending(s => s.GoalDifference)
                          .ToList()
                );

            var viewModel = new GroupStandingsViewModel
            {
                Groups = groupedStandings
            };

            return View(viewModel);
        }
    }
}