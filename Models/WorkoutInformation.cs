using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace BeFit.Models
{
    public class WorkoutInformation
    {
        public int Id { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public int Series { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int TrainingSessionId { get; set; }
        
        [ForeignKey("TrainingSessionId")]
        public TrainingSession TrainingSession { get; set; }
    }
}
