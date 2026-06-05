using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();

            var standings = teams
                .GroupBy(t => t.GroupLetter)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(t => new TeamStanding { Team = t }).ToList()
                );

            foreach (var match in matches.Where(m => m.Status == "Finished" && m.HomeScore.HasValue && m.AwayScore.HasValue))
            {
                var homeStanding = standings[match.HomeTeam.GroupLetter].FirstOrDefault(s => s.Team.Id == match.HomeTeamId);
                var awayStanding = standings[match.AwayTeam.GroupLetter].FirstOrDefault(s => s.Team.Id == match.AwayTeamId);

                if (homeStanding != null && awayStanding != null)
                {
                    homeStanding.MatchesPlayed++;
                    awayStanding.MatchesPlayed++;
                    homeStanding.GoalsFor += match.HomeScore.Value;
                    homeStanding.GoalsAgainst += match.AwayScore.Value;
                    awayStanding.GoalsFor += match.AwayScore.Value;
                    awayStanding.GoalsAgainst += match.HomeScore.Value;

                    if (match.HomeScore.Value > match.AwayScore.Value)
                    {
                        homeStanding.Wins++;
                        awayStanding.Losses++;
                    }
                    else if (match.HomeScore.Value < match.AwayScore.Value)
                    {
                        awayStanding.Wins++;
                        homeStanding.Losses++;
                    }
                    else
                    {
                        homeStanding.Draws++;
                        awayStanding.Draws++;
                    }
                }
            }

            foreach (var groupKey in standings.Keys.ToList())
            {
                standings[groupKey] = standings[groupKey]
                    .OrderByDescending(s => s.Points)
                    .ThenByDescending(s => s.GoalDifference)
                    .ThenByDescending(s => s.GoalsFor)
                    .ToList();
            }

            var viewModel = new WorldCupViewModel
            {
                Groups = standings.OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.Value),
                Matches = matches
            };

            return View(viewModel);
        }
    }
}