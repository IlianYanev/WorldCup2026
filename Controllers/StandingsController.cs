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
            
            // Взимаме само мачовете от груповата фаза (Id <= 72)
            var matches = await _context.Matches
                .Where(m => m.Status == "Finished" && m.Id <= 72)
                .ToListAsync();

            var viewModel = new GroupStandingsViewModel();
            var groupedTeams = teams.GroupBy(t => t.GroupLetter).OrderBy(g => g.Key);

            foreach (var group in groupedTeams)
            {
                var standingsList = new List<TeamStandingViewModel>();
                foreach (var team in group)
                {
                    var standing = new TeamStandingViewModel { Team = team, Points = 0, GoalDifference = 0 };
                    
                    foreach (var match in matches)
                    {
                        if (match.HomeTeamId == team.Id)
                        {
                            standing.GoalDifference += (match.HomeScore ?? 0) - (match.AwayScore ?? 0);
                            if (match.HomeScore > match.AwayScore) standing.Points += 3;
                            else if (match.HomeScore == match.AwayScore) standing.Points += 1;
                        }
                        else if (match.AwayTeamId == team.Id)
                        {
                            standing.GoalDifference += (match.AwayScore ?? 0) - (match.HomeScore ?? 0);
                            if (match.AwayScore > match.HomeScore) standing.Points += 3;
                            else if (match.AwayScore == match.HomeScore) standing.Points += 1;
                        }
                    }
                    standingsList.Add(standing);
                }
                viewModel.Groups.Add(group.Key, standingsList.OrderByDescending(s => s.Points).ThenByDescending(s => s.GoalDifference).ToList());
            }

            ViewBag.AllKnockoutMatches = await _context.Matches
                .Where(m => m.Id >= 73)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .OrderBy(m => m.Id)
                .ToListAsync();

            return View(viewModel);
        }
    }
}