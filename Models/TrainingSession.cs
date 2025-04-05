using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime? EndTime { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public ICollection<WorkoutInformation> Workouts { get; set; } = new List<WorkoutInformation>();

        [Required]
        [Display(Name = "Created by")]
        public string CreatedById { get; set; }  // Foreign key for AppUser

        [ForeignKey("CreatedById")]
        public virtual AppUser? CreatedBy { get; set; }  // Navigation property to AppUser
    }
}
