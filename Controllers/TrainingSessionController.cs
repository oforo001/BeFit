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

            var workout = trainingSession.Workouts.FirstOrDefault();

            var model = new EditTrainingSessionViewModel
            {
                Id = trainingSession.Id,
                ExerciseName = trainingSession.Exercise.Name,
                StartTime = trainingSession.StartTime,
                EndTime = trainingSession.EndTime,
            
                Series = workout?.Series,
                Count = workout?.Count,
                Weight = workout?.Weight
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTrainingSessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trainingSession = await _applicationDbContext.TrainingSessions
                    .Include(ts => ts.Exercise)
                    .Include(ts => ts.Workouts)
                    .FirstOrDefaultAsync(ts => ts.Id == model.Id);

                if (trainingSession == null)
                {
                    return NotFound();
                }
                if (trainingSession.Exercise.Name != model.ExerciseName)
                {
                    trainingSession.Exercise.Name = model.ExerciseName;
                }

                trainingSession.StartTime = model.StartTime;
                trainingSession.EndTime = model.EndTime;

                var workout = trainingSession.Workouts.FirstOrDefault();
                if (workout != null)
                {
                    workout.Series = model.Series ?? 0;
                    workout.Count = model.Count ?? 0;  
                    workout.Weight = (float)(model.Weight ?? 0.0);
                }
                else
                {
                    trainingSession.Workouts.Add(new WorkoutInformation
                    {
                        Series = model.Series ?? 0,  
                        Count = model.Count ?? 0,
                        Weight = (float)(model.Weight ?? 0.0),
                        TrainingSessionId = trainingSession.Id 
                    });
                }

                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
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

