using BeFit.Models;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           
        }
       public DbSet<Exercise> Exercises { get; set; }
       public  DbSet<TrainingSession> TrainingSessions { get; set; }
       public DbSet<WorkoutInformation> WorkoutInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Exercise)
                .WithMany(e => e.TrainingSessions)
                .HasForeignKey(ts => ts.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutInformation>()
                .HasOne(wi => wi.TrainingSession)
                .WithMany(ts => ts.Workouts)
                .HasForeignKey(wi => wi.TrainingSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            //data seeding for DataBase

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Squat" },
                new Exercise { Id = 2, Name = "Bench Press" },
                new Exercise { Id = 3, Name = "Deadlift" }
            );
            modelBuilder.Entity<TrainingSession>().HasData(
                new TrainingSession { Id = 1, ExerciseId = 1, StartTime = new DateTime(2025, 3, 23, 8, 0, 0), EndTime = new DateTime(2025, 3, 23, 9, 0, 0) },
                new TrainingSession { Id = 2, ExerciseId = 2, StartTime = new DateTime(2025, 3, 23, 9, 0, 0), EndTime = new DateTime(2025, 3, 23, 10, 0, 0) },
                new TrainingSession { Id = 3, ExerciseId = 3, StartTime = new DateTime(2025, 3, 23, 10, 0, 0), EndTime = new DateTime(2025, 3, 23, 11, 0, 0) }
            );


            modelBuilder.Entity<WorkoutInformation>().HasData(
                new WorkoutInformation { Id = 1, TrainingSessionId = 1, Weight = 100, Series = 3, Count = 8 },
                new WorkoutInformation { Id = 2, TrainingSessionId = 2, Weight = 85, Series = 3, Count = 10 },
                new WorkoutInformation { Id = 3, TrainingSessionId = 3, Weight = 125, Series = 3, Count = 6 }
                );

        }
    }
}
