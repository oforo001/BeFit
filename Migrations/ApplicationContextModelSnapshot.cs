﻿// <auto-generated />
using System;
using BeFit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeFit.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("BeFit.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exercises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Squat"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bench Press"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Deadlift"
                        });
                });

            modelBuilder.Entity("BeFit.Models.TrainingSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("TrainingSessions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndTime = new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            ExerciseId = 1,
                            StartTime = new DateTime(2025, 3, 23, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            EndTime = new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            ExerciseId = 2,
                            StartTime = new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            EndTime = new DateTime(2025, 3, 23, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            ExerciseId = 3,
                            StartTime = new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BeFit.Models.WorkoutInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Series")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainingSessionId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("TrainingSessionId");

                    b.ToTable("WorkoutInformations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 8,
                            Series = 3,
                            TrainingSessionId = 1,
                            Weight = 100f
                        },
                        new
                        {
                            Id = 2,
                            Count = 10,
                            Series = 3,
                            TrainingSessionId = 2,
                            Weight = 85f
                        },
                        new
                        {
                            Id = 3,
                            Count = 6,
                            Series = 3,
                            TrainingSessionId = 3,
                            Weight = 125f
                        });
                });

            modelBuilder.Entity("BeFit.Models.TrainingSession", b =>
                {
                    b.HasOne("BeFit.Models.Exercise", "Exercise")
                        .WithMany("TrainingSessions")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("BeFit.Models.WorkoutInformation", b =>
                {
                    b.HasOne("BeFit.Models.TrainingSession", "TrainingSession")
                        .WithMany("Workouts")
                        .HasForeignKey("TrainingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingSession");
                });

            modelBuilder.Entity("BeFit.Models.Exercise", b =>
                {
                    b.Navigation("TrainingSessions");
                });

            modelBuilder.Entity("BeFit.Models.TrainingSession", b =>
                {
                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
