using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace BeFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationDbContext;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationDbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var loggedUser = await _userManager.GetUserAsync(User);

                if (loggedUser != null)
                {
                    var sessions = await _applicationDbContext.TrainingSessions
                        .Include(trainingSession => trainingSession.Exercise)
                        .Include(trainingSession => trainingSession.Workouts)
                        .Where(trainingSession => trainingSession.CreatedById == loggedUser.Id)
                        .ToListAsync();

                    ViewBag.TrainingSessions = sessions;
                }

                ViewBag.Username = User.Identity.Name;
            }

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
