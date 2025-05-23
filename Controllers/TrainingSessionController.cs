﻿using BeFit.Data;
using BeFit.Models;
using BeFit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _applicationDbContext.TrainingSessions
                .Include(ts => ts.Exercise)
                .Include(ts => ts.Workouts)
                .FirstOrDefaultAsync(ts => ts.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            _applicationDbContext.WorkoutInformations.RemoveRange(session.Workouts);
            _applicationDbContext.TrainingSessions.Remove(session);

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var exercises = await _applicationDbContext.Exercises
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                })
                .ToListAsync();

            if (exercises == null)
            {
                exercises = new List<SelectListItem>();
            }
            var model = new CreateTrainingSessionViewModel
            {
                Exercises = exercises
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int selectedExerciseId, DateTime startTime, DateTime? endTime, int series, int count, float weight)
        {
            var exercise = await _applicationDbContext.Exercises
                .FirstOrDefaultAsync(e => e.Id == selectedExerciseId);

            if (exercise == null)
            {
                ModelState.AddModelError("selectedExerciseId", "Please select a valid exercise.");
            }
            if (endTime.HasValue && endTime <= startTime)
            {
                ModelState.AddModelError("endTime", "End time must be after the start time.");
            }
            if (!ModelState.IsValid)
            {
                var exercises = await _applicationDbContext.Exercises
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Name
                    })
                    .ToListAsync();
                ViewBag.Exercises = exercises;
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This assumes you are using Identity and that the user is authenticated

            var trainingSession = new TrainingSession
            {
                ExerciseId = exercise.Id,
                StartTime = startTime,
                EndTime = endTime,
                CreatedById = userId,
                Workouts = new List<WorkoutInformation>
        {
            new WorkoutInformation
            {
                Series = series,
                Count = count,
                Weight = weight
            }
        }
            };

            _applicationDbContext.TrainingSessions.Add(trainingSession);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}

