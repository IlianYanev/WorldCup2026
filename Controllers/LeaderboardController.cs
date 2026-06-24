using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;
using WorldCup2026.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldCup2026.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Where(u => u.Username != "admin").ToListAsync();
            var allGroupPreds = await _context.GroupPredictions.Include(p => p.Team).ToListAsync();
            var allPodiumPreds = await _context.ThirdPlacePredictions.ToListAsync();
            
            var actualStandings = await GetActualStandingsDict();

            var matches = await _context.Matches.ToListAsync();
            var finalMatch = matches.FirstOrDefault(m => m.Id == 104);
            var thirdMatch = matches.FirstOrDefault(m => m.Id == 103);

            var model = new LeaderboardViewModel();

            foreach (var user in users)
            {
                var score = new UserScoreViewModel { Username = user.Username };
                
                score.GroupPoints = CalculateGroupPoints(allGroupPreds.Where(p => p.UserId == user.Id).ToList(), actualStandings);

                score.PodiumPoints = CalculatePodiumPoints(allPodiumPreds.Where(p => p.UserId == user.Id).ToList(), finalMatch, thirdMatch);

                score.TotalPoints = score.GroupPoints + score.PodiumPoints;
                model.UserScores.Add(score);
            }

            model.UserScores = model.UserScores.OrderByDescending(s => s.TotalPoints).ToList();
            return View(model);
        }

        private int CalculateGroupPoints(List<GroupPrediction> userPreds, Dictionary<string, List<int>> actualStandings)
        {
            int total = 0;
            foreach (var group in userPreds.GroupBy(p => p.Team.GroupLetter))
            {
                string letter = group.Key;
                if (!actualStandings.ContainsKey(letter)) continue;

                List<int> realOrder = actualStandings[letter];
                int correctCount = 0;

                foreach (var pred in group)
                {
                    if (pred.PredictedPosition >= 1 && pred.PredictedPosition <= 4)
                    {
                        if (pred.TeamId == realOrder[pred.PredictedPosition - 1])
                        {
                            total += 1;
                            correctCount++;
                        }
                    }
                }
                if (correctCount == 4) total += 3;
            }
            return total;
        }

        private int CalculatePodiumPoints(List<ThirdPlacePrediction> userPreds, Match final, Match third)
        {
            if (final == null || third == null || final.Status != "Finished" || third.Status != "Finished") return 0;

            int points = 0;
            int actualWinner = (final.HomeScore > final.AwayScore) ? final.HomeTeamId : final.AwayTeamId;
            int actualRunnerUp = (final.HomeScore > final.AwayScore) ? final.AwayTeamId : final.HomeTeamId;
            int actualThird = (third.HomeScore > third.AwayScore) ? third.HomeTeamId : third.AwayTeamId;

            var p1 = userPreds.FirstOrDefault(p => p.Rank == 1);
            var p2 = userPreds.FirstOrDefault(p => p.Rank == 2);
            var p3 = userPreds.FirstOrDefault(p => p.Rank == 3);

            if (p1 != null && p1.TeamId == actualWinner) points += 8;
            if (p2 != null && p2.TeamId == actualRunnerUp) points += 5;
            if (p3 != null && p3.TeamId == actualThird) points += 3;

            return points;
        }

        private async Task<Dictionary<string, List<int>>> GetActualStandingsDict()
        {
            var teams = await _context.Teams.ToListAsync();
            var matches = await _context.Matches.Where(m => m.Status == "Finished" && m.Id <= 72).ToListAsync();
            
            var dict = new Dictionary<string, List<int>>();
            var groupedTeams = teams.GroupBy(t => t.GroupLetter);

            foreach (var group in groupedTeams)
            {
                var standingsList = new List<TeamStandingHelper>();
                foreach (var team in group)
                {
                    var helper = new TeamStandingHelper { TeamId = team.Id, Points = 0, GoalDifference = 0 };
                    foreach (var match in matches)
                    {
                        if (match.HomeTeamId == team.Id)
                        {
                            helper.GoalDifference += (match.HomeScore ?? 0) - (match.AwayScore ?? 0);
                            if (match.HomeScore > match.AwayScore) helper.Points += 3;
                            else if (match.HomeScore == match.AwayScore) helper.Points += 1;
                        }
                        else if (match.AwayTeamId == team.Id)
                        {
                            helper.GoalDifference += (match.AwayScore ?? 0) - (match.HomeScore ?? 0);
                            if (match.AwayScore > match.HomeScore) helper.Points += 3;
                            else if (match.AwayScore == match.HomeScore) helper.Points += 1;
                        }
                    }
                    standingsList.Add(helper);
                }

                var orderedTeamIds = standingsList
                    .OrderByDescending(s => s.Points)
                    .ThenByDescending(s => s.GoalDifference)
                    .Select(s => s.TeamId)
                    .ToList();

                dict[group.Key] = orderedTeamIds;
            }

            return dict;
        }

        private class TeamStandingHelper
        {
            public int TeamId { get; set; }
            public int Points { get; set; }
            public int GoalDifference { get; set; }
        }
    }
}