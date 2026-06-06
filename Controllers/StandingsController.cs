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
            var viewModel = new GroupStandingsViewModel();

            var groupedTeams = teams.GroupBy(t => t.GroupLetter).OrderBy(g => g.Key);

            foreach (var group in groupedTeams)
            {
                var standingsList = new List<TeamStandingViewModel>();
                foreach (var team in group)
                {
                    standingsList.Add(new TeamStandingViewModel
                    {
                        Team = team,
                        Points = 0,
                        GoalDifference = 0
                    });
                }
                viewModel.Groups.Add(group.Key, standingsList);
            }

            return View(viewModel);
        }
    }
}