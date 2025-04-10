using BeFit.Data;
using BeFit.Models;
using BeFit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeFit.Controllers
{
    public class TrainingSessionController : Controller
    {
        public readonly ApplicationContext _applicationDbContext;
        public readonly UserManager<AppUser> _userManager;

        public TrainingSessionController(UserManager<AppUser> userManager, ApplicationContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

       
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trainingSession = await _applicationDbContext.TrainingSessions
                .Include(ts => ts.Exercise)
                .Include(ts => ts.Workouts)
                .FirstOrDefaultAsync(ts => ts.Id == id);

            if (trainingSession == null)
            {
                return NotFound();
            }
            var model = new EditTrainingSessionViewModel
            {
                Id = trainingSession.Id,
                ExerciseName = trainingSession.Exercise.Name,
                StartTime = trainingSession.StartTime,
                EndTime = trainingSession.EndTime,
                Workouts = trainingSession.Workouts.Select(workout => new WorkoutInformation
                {
                    Id = workout.Id,
                    Series = workout.Series,
                    Count = workout.Count,
                    Weight = workout.Weight
                }).ToList()
            };

            return View(model);
        }



        public IActionResult Delete()
        {
            return View();
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
