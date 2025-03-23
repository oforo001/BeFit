using System.ComponentModel.DataAnnotations;



namespace BeFit.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }
}
