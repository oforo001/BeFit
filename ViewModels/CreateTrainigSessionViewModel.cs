using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class CreateTrainingSessionViewModel
{
    [Required(ErrorMessage = "Exercise is required.")]
    public int SelectedExerciseId { get; set; }

    // Initialize Exercises as an empty list to avoid null reference exceptions
    public List<SelectListItem> Exercises { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "Start time is required.")]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? EndTime { get; set; }

    [Required(ErrorMessage = "Series is required.")]
    [Range(1, 100, ErrorMessage = "Series must be between 1 and 100.")]
    public int Series { get; set; }

    [Required(ErrorMessage = "Count is required.")]
    [Range(1, 1000, ErrorMessage = "Count must be between 1 and 1000.")]
    public int Count { get; set; }

    [Required(ErrorMessage = "Weight is required.")]
    [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kg.")]
    public float Weight { get; set; }

    // Custom validation to ensure EndTime is after StartTime
    public bool IsValidEndTime => !EndTime.HasValue || EndTime > StartTime;
}
