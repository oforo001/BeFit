using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeFit.Models;

namespace BeFit.ViewModels
{
    public class EditTrainingSessionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Exercise name is required.")]
        [Display(Name = "Exercise Name")]
        public string ExerciseName { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        public List<WorkoutInformation> Workouts { get; set; } = new List<WorkoutInformation>();

        
        [Required(ErrorMessage = "Please enter the number of series.")]
        [Range(0, 20, ErrorMessage = "Add right value")]
        public int? Series { get; set; }

        [Required(ErrorMessage = "Please enter the count of repetitions.")]
        public int? Count { get; set; }

        [Required(ErrorMessage = "Please enter the weight.")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive value.")]
        public double? Weight { get; set; }
    }
}
