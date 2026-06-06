using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCup2026.Data;

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
        public IActionResult SavePredictions(string groupOrders)
        {
            return RedirectToAction("Index");
        }
    }
}