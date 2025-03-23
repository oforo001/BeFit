using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeFit.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    Series = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingSessionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutInformations_TrainingSessions_TrainingSessionId",
                        column: x => x.TrainingSessionId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Squat" },
                    { 2, "Bench Press" },
                    { 3, "Deadlift" }
                });

            migrationBuilder.InsertData(
                table: "TrainingSessions",
                columns: new[] { "Id", "EndTime", "ExerciseId", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 3, 23, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 3, 23, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 3, 23, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "WorkoutInformations",
                columns: new[] { "Id", "Count", "Series", "TrainingSessionId", "Weight" },
                values: new object[,]
                {
                    { 1, 8, 3, 1, 100f },
                    { 2, 10, 3, 2, 85f },
                    { 3, 6, 3, 3, 125f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_ExerciseId",
                table: "TrainingSessions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutInformations_TrainingSessionId",
                table: "WorkoutInformations",
                column: "TrainingSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutInformations");

            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
